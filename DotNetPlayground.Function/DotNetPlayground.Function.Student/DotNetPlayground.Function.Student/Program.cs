using AutoMapper;
using DotNetPlayground;
using DotNetPlayground.Function.Student.Repositories;
using DotNetPlayground.Function.Student.Services;
using DotNetPlayground.Repositories;
using DotNetPlayground.Services;
using DotNetPlayground.SqlServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var mappingConfig = new MapperConfiguration(mc =>
{  
    mc.AddProfile(new StudentAutoMapperProfile());
});

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices((hostContext, services) =>
    {
        var config = hostContext.Configuration;
        var connectionString = config.GetConnectionString("Default");

        services.AddAutoMapper(typeof(StudentAutoMapperProfile).Assembly);
        services.AddDbContext<StudentDbContext>(options =>
            options.UseSqlServer(connectionString ?? throw new InvalidOperationException("Connection string is not set.")));
        
        services.AddScoped<IStudentService, StudentService>();
        services.AddScoped<IStudentRepository, StudentRepository>();
    })
    .Build();
host.Run();