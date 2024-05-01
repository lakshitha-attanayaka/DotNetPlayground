using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace DotNetPlayground.SqlServer;

public class DbMigratorHostedService(IHostApplicationLifetime hostApplicationLifetime, IConfiguration configuration)
    : IHostedService
{
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("Starting migration...");
        var studentOptionsBuilder = new DbContextOptionsBuilder<StudentDbContext>();
        studentOptionsBuilder.UseSqlServer(configuration.GetConnectionString("Default"));
        var courseOptionsBuilder = new DbContextOptionsBuilder<CourseDbContext>();
        courseOptionsBuilder.UseSqlServer(configuration.GetConnectionString("Default"));

        await using var studentDbContext = new StudentDbContext(studentOptionsBuilder.Options);
        await studentDbContext.Database.MigrateAsync(cancellationToken);
        await using var courseDbContext = new CourseDbContext(courseOptionsBuilder.Options);
        await courseDbContext.Database.MigrateAsync(cancellationToken);
            
        Console.WriteLine("Migration completed.");
        hostApplicationLifetime.StopApplication();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}