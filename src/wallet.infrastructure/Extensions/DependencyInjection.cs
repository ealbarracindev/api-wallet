using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.OpenApi.Models;
using wallet.infrastructure.Configurations;
using wallet.infrastructure.Constants;
using wallet.infrastructure.Data;
using wallet.infrastructure.Services.UserService;
using wallet.infrastructure.Services.WalletService;

namespace wallet.infrastructure.Extensions;

public static class DependencyInjection
{
    #region Config Context
    public static IServiceCollection AddCustomDbContextsPool(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContextPool<ApplicationDbContext>(options =>
           options.UseSqlServer(configuration.GetConnectionString("WalletApi"))
       );

        return services;
    }
    #endregion
    #region Policies cors
    public static IServiceCollection AddCustomCors(this IServiceCollection services, IConfiguration configuration)
    {
        var policy = configuration.GetConnectionString("WalletApi");
        //CORS
        services.AddCors(o => o.AddPolicy(name: CorsPolicyConstant.CorsPolicy, builder =>

        {
            builder.AllowAnyHeader()
                   .WithMethods(configuration.GetSection("AllowedMethod").Get<string[]>())
                   .WithOrigins(configuration.GetSection("AllowedOrigin").Get<string[]>())
                   .AllowCredentials();
        }));
        return services;
    }
    #endregion
    #region Config Dependencies injection, controller
    public static IServiceCollection AddCustomServices(this IServiceCollection services)
    {
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IWalletService, WalletService>();

        return services;
    }
    #endregion
    #region Config Api version
    public static IServiceCollection AddCustomApiVersioning(this IServiceCollection services)
    {
        services.AddApiVersioning(x =>
        {
            x.DefaultApiVersion = new ApiVersion(1, 0);
            x.AssumeDefaultVersionWhenUnspecified = true;
            x.ReportApiVersions = true; //informar al cliente si esta deprecate entre otros
            x.ApiVersionReader = new HeaderApiVersionReader("x-api-version");
        });
        return services;
    }
    #endregion
    #region Config Swagger
    public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(swagger =>
        {
            //This is to generate the Default UI of Swagger Documentation  
            swagger.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Api Wallet | SC",
                Description = "Web API Construida en NET 7",
                License = new OpenApiLicense()
                {
                    Name = "MIT",
                    Url = new Uri("https://opensource.org/licenses/MIT")
                },
                Contact = new OpenApiContact()
                {
                    Name = "javier",
                    Email = "ealbarracindev@gmail.com"
                }
            });
            // To Enable authorization using Swagger (JWT)  
            //swagger.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            //{
            //    Name = "Authorization",
            //    Type = SecuritySchemeType.ApiKey,
            //    Scheme = "Bearer",
            //    BearerFormat = "JWT",
            //    In = ParameterLocation.Header,
            //    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
            //});
            //swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
            //{
            //    {
            //          new OpenApiSecurityScheme
            //            {
            //                Reference = new OpenApiReference
            //                {
            //                    Type = ReferenceType.SecurityScheme,
            //                    Id = "Bearer"
            //                }
            //            },
            //            new string[] {}

            //    }
            //});

        });

        return services;
    }
    #endregion
    #region Config Health Check
    public static IServiceCollection AddCustomHealthChecks(this IServiceCollection services, IConfiguration configuration) =>
        services.AddHealthChecks()
                .AddSqlServer(configuration.GetConnectionString("WalletApi"),
                    tags: new[] { "dependencies" })
                //.AddRedis("redis",
                //    tags: new[] { "dependencies" })

                .AddCheck("self", () => HealthCheckResult.Healthy())
                .Services;
    #endregion

}
