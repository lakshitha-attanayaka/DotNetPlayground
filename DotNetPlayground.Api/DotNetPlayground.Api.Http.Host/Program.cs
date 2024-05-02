using AutoMapper;
using DotNetPlayground;
using DotNetPlayground.Api.Repositories;
using DotNetPlayground.Api.Services;
using DotNetPlayground.Models;
using DotNetPlayground.Repositories;
using DotNetPlayground.Services;
using DotNetPlayground.SqlServer;
using Keycloak.AuthServices.Authentication;
using Keycloak.AuthServices.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddKeycloakAuthentication(new KeycloakAuthenticationOptions()
        {
            // Keycloak server URL
            AuthServerUrl = builder.Configuration["Keycloak:AuthServerUrl"]!,
            // Realm Name
            Realm = builder.Configuration["Keycloak:Realm"]!,
            // ClientId
            Resource = builder.Configuration["Keycloak:Resource"]!,

            SslRequired = builder.Configuration["Keycloak:SslRequired"]!,
            VerifyTokenAudience = false,
        });

        builder.Services.AddKeycloakAuthorization(new KeycloakProtectionClientOptions()
        {
            AuthServerUrl = builder.Configuration["Keycloak:AuthServerUrl"]!,
            Realm = builder.Configuration["Keycloak:Realm"]!,
            Resource = builder.Configuration["Keycloak:Resource"]!,
            SslRequired = builder.Configuration["Keycloak:SslRequired"]!,
            VerifyTokenAudience = false
        });

        // Add services to the container.
        builder.Services.AddAuthorization();
        builder.Services.AddDbContext<StudentDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
        builder.Services.AddDbContext<CourseDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

        var mappingConfig = new MapperConfiguration(mc =>
        {  
            mc.AddProfile(new StudentAutoMapperProfile());
            mc.AddProfile(new CourseAutoMapperProfile());
        });
        
        builder.Services.AddAutoMapper([
            typeof(StudentAutoMapperProfile),
            typeof(CourseAutoMapperProfile)
        ]);

        
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddControllers();
        
        builder.Services.AddSwaggerGen(c =>
        {
            var securityScheme = new OpenApiSecurityScheme
            {
                Name = "Keycloak",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.OpenIdConnect,
                OpenIdConnectUrl = new Uri($"{builder.Configuration["Keycloak:AuthServerUrl"]}realms/{builder.Configuration["Keycloak:Realm"]}/.well-known/openid-configuration"),
                Scheme = "bearer",
                BearerFormat = "JWT",
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            };
            c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {securityScheme, Array.Empty<string>()}
            });
        });
        
        // Register Services
        RegisterServices(builder.Services);

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if(bool.TryParse(builder.Configuration["App:EnableSwagger"] ?? "false", out var enableSwagger) && enableSwagger)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseCors(a => a.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());

        app.UseAuthentication();
        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }

    private static void RegisterServices(IServiceCollection builderServices)
    {
        builderServices.AddScoped<IStudentService, StudentService>();
        builderServices.AddScoped<IStudentRepository, StudentRepository>();
        builderServices.AddScoped<ICourseService, CourseService>();
        builderServices.AddScoped<ICourseRepository, CourseRepository>();
    }
}