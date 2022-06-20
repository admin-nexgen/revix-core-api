using System;
using System.IO;
using System.Reflection;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Revix.Core.Infrastructure.Common.Filters;

namespace Revix.Core.Api.Extensions;

public static class StartupExtensions
{
    #region ConfigureService
        
    public static IServiceCollection AddCustomCors(this IServiceCollection services)
    {
        services.AddCors(options => {
            options.AddPolicy("CorsPolicy",
                policy => policy
                    .SetIsOriginAllowed(_ => true)
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
            );
        });

        return services;
    }
        
    public static IServiceCollection AddCustomMvc(this IServiceCollection services)
    {
        services
            .AddControllers(options =>
            {
                options.Filters.Add<HttpGlobalExceptionFilter>();
            })
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.Formatting = Formatting.Indented;
                options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                options.SerializerSettings.Converters.Add(new StringEnumConverter { NamingStrategy = new CamelCaseNamingStrategy() });
            })
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.WriteIndented = true;
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });

        return services;
    }

    public static IServiceCollection AddCustomApiFeatures(this IServiceCollection services)
        => services
            // .AddValidationErrorLogging()
            .AddVersionedApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            })
            .AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
            })
            .AddRouting(options =>
            {
                options.LowercaseUrls = true;
            });


    public static IServiceCollection AddCustomSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Revix.Core.Api", Version = "v1" });
            
            // Set the comments path for the Swagger JSON and UI.
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlPath);
        });

        services.AddSwaggerGenNewtonsoftSupport(); // explicit opt-in - needs to be placed after AddSwaggerGen()

        return services;
    }

    public static IServiceCollection AddCustomHealthChecks(this IServiceCollection services)
    {
        services
            .AddHealthChecks();

        // NOTE: More health checks can be added here

        return services;
    }

    #endregion
        
    #region ConfigureApp
        
    public static IApplicationBuilder UseCustomCors(this IApplicationBuilder app)
    {
        app.UseCors("CorsPolicy");

        return app;
    }

    public static IApplicationBuilder UseCustomSwagger(
        this IApplicationBuilder app,
        IApiVersionDescriptionProvider provider)
    {
        app.UseSwagger(options => options.RouteTemplate = "/{documentName}/swagger.json");
            
        app.UseSwaggerUI(options =>
        {
            foreach (var versionDescription in provider.ApiVersionDescriptions)
            {
                options.SwaggerEndpoint("/" + versionDescription.GroupName + "/swagger.json", versionDescription.GroupName.ToUpperInvariant());
                options.RoutePrefix = string.Empty;
            }
        });
        return app;
    }
        
    #endregion
}