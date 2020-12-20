using System;
using System.IO;
using System.Linq;
using System.Reflection;
using CanDatabase.Application.CanDbs.Commands.ParseDbcFile;
using CanDatabase.Application.Infrastructure;
using CanDatabase.Common.Constants;
using CanDatabase.Domain.IServices;
using CanDatabase.Domain.Services;
using CanDatabase.Persistence.Database;
using CanDatabase.WebApi.Filters;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace CanDatabase.WebApi
{
    /// <summary>
    /// 
    /// </summary>
    public sealed partial class Startup
    {
        /// <summary>
        /// Configures Applications DI IoC Container
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            AddWebApi(services);
            AddEssentials(services);
            AddMediatRServices(services);
            AddApplicationServices(services);
            AddPersistence(services);
            AddSwagger(services);
        }

        /// <summary>
        /// Essential Services
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        private IServiceCollection AddEssentials(IServiceCollection services)
        {
            services.AddSingleton(serviceProvider => _webApiConfiguration);

            return services;
        }

        /// <summary>
        /// Web Api Services 
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        private IServiceCollection AddWebApi(IServiceCollection services)
        {
            services
                .AddControllersWithViews(mvcOptions =>
                {
                    mvcOptions.Filters.Add(typeof(CustomExceptionFilterAttribute));
                })
                .AddJsonOptions(jsonOptions =>
                {
                    _webApiConfiguration
                        .SystemTextJsonSerializerConfiguration
                        .MapJsonSerializerOptionsToDefaultOptions(
                            jsonSerializerOptions: jsonOptions.JsonSerializerOptions
                        );
                })
                .SetCompatibilityVersion(CompatibilityVersion.Latest)
                .AddFluentValidation(
                    fluentValidatorConfiguration =>
                        fluentValidatorConfiguration
                            .RegisterValidatorsFromAssemblyContaining<ParseDbcFileValidator>()
                );

            services.AddRazorPages();

            services.AddHealthChecks();

            services
                .AddApiVersioning(apiVersioningOptions =>
                {
                    apiVersioningOptions.DefaultApiVersion = new ApiVersion(
                        majorVersion: 1,
                        minorVersion: 0
                    );
                    apiVersioningOptions.AssumeDefaultVersionWhenUnspecified = true;
                    apiVersioningOptions.ReportApiVersions = true;
                    apiVersioningOptions.ApiVersionReader = new HeaderApiVersionReader(HttpHeaderConstants.VersionHeaderName);
                });

            return services;
        }

        /// <summary>
        /// Mediator Services
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddMediatRServices(IServiceCollection services)
        {
            services.AddMediatR(typeof(ParseDbcFileCommand));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ExceptionRequestPipeline<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggerRequestPipeline<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipeline<,>));

            return services;
        }

        /// <summary>
        /// Application Services: SlackService, TrendingScoreService, LoggerService
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddApplicationServices(IServiceCollection services)
        {
            services.AddScoped<IParseDbcFileService, ParseDbcFileService>();

            return services;
        }

        /// <summary>
        /// Persistence Services
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        private IServiceCollection AddPersistence(IServiceCollection services)
        {
            services.AddDbContext<ICanDatabaseContext, CanDatabaseContext>(options =>
            {
                options.UseSqlServer(
                    connectionString: _webApiConfiguration.DatabaseConfiguration.ConnectionString,
                    sqlServerOptionsAction: sqlServerOptions =>
                    {
                        sqlServerOptions.CommandTimeout(_webApiConfiguration.DatabaseConfiguration.CommandTimeoutInSeconds);
                    }
                );
                options.UseQueryTrackingBehavior(_webApiConfiguration.DatabaseConfiguration.QueryTrackingBehavior);
                options.EnableDetailedErrors(_webApiConfiguration.DatabaseConfiguration.EnableDetailedErrors);
                options.EnableSensitiveDataLogging(_webApiConfiguration.DatabaseConfiguration.EnableSensitiveDataLogging);
            });

            return services;
        }

        /// <summary>
        /// Adds swagger API documentation
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        private IServiceCollection AddSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                ConfigureSwaggerDocumentVersions(options);
                ConfigureDocumentGenerationPath(options);
            });

            void ConfigureSwaggerDocumentVersions(SwaggerGenOptions options)
            {
                foreach (var apiVersion in _webApiConfiguration.AppConfiguration.ApiVersions)
                {
                    options.SwaggerDoc(apiVersion.ToString(), new OpenApiInfo
                    {
                        Title = $"CanDatabase API",
                        Version = apiVersion.ToString(),
                        Description = "CanDatabase APP Web Api",
                    });
                }

                options.DocInclusionPredicate((version, desc) =>
                {
                    var apiVersions = desc
                        .ActionDescriptor
                        .GetApiVersionModel()
                        .DeclaredApiVersions
                        .Select(supportedApiVersion => supportedApiVersion.ToString());

                    return apiVersions.Any(v => version.Equals(v, StringComparison.InvariantCultureIgnoreCase));
                });
            }


            static void ConfigureDocumentGenerationPath(SwaggerGenOptions options)
            {
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);
            }

            return services;
        }

    }
}
