namespace EGAH.EventGenerator.Configuration;

//using EGAH.Common.Security;
//using EGAH.Services.Settings;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using System.Reflection;

/// <summary>
/// Swagger configuration
/// </summary>
public static class SwaggerConfiguration
{
    private static string AppTitle = "EGAH.EventGenerator";

    /// <summary>
    /// Add OpenAPI for API
    /// </summary>
    /// <param name="services">Services collection</param>
    /// <param name="identitySettings"></param>
    /// <param name="swaggerSettings"></param>
    public static IServiceCollection AddAppSwagger(this IServiceCollection services/*, IdentitySettings identitySettings, SwaggerSettings swaggerSettings*/)
    {
        //// Если Swagger выключен, то его настраивать не нужно
        //if (!swaggerSettings.Enabled)
        //    return services;

        services
            .AddOptions<SwaggerGenOptions>()
            .Configure<IApiVersionDescriptionProvider>((options, provider) =>
            {
                foreach (var avd in provider.ApiVersionDescriptions) // Подключение документирующих тегов (///)
                {
                    options.SwaggerDoc(avd.GroupName, new OpenApiInfo
                    {
                        Version = avd.GroupName,
                        Title = $"{AppTitle}"
                    });
                }
            });

        services.AddSwaggerGen(options =>
        {
            options.SupportNonNullableReferenceTypes();

            options.UseInlineDefinitionsForEnums();

            options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

            options.DescribeAllParametersInCamelCase();

            var xmlFile = "EventGenerator.xml"; // Указать в свойствах проекта создание файла документации API!!!
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            options.IncludeXmlComments(xmlPath);

            options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
            {
                Name = "Bearer",
                Type = SecuritySchemeType.OAuth2,
                Scheme = "oauth2",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Flows = new OpenApiOAuthFlows
                {
                    //Password = new OpenApiOAuthFlow
                    //{
                    //    TokenUrl = new Uri($"{identitySettings.Url}/connect/token"),
                    //    Scopes = new Dictionary<string, string>
                    //    {
                            
                    //    }
                    //}
                }
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "oauth2"
                            }
                        },
                        new List<string>()
                    }
                });

            options.UseOneOfForPolymorphism();
            options.EnableAnnotations(enableAnnotationsForInheritance: true, enableAnnotationsForPolymorphism: true);

            options.ExampleFilters();
        });

        services.AddSwaggerExamplesFromAssemblies(Assembly.GetEntryAssembly());

        services.AddSwaggerGenNewtonsoftSupport();

        return services;
    }


    /// <summary>
    /// Start OpenAPI UI
    /// </summary>
    /// <param name="app">Web application</param>
    public static void UseAppSwagger(this WebApplication app)
    {
//        var swaggerSettings = app.Services.GetService<SwaggerSettings>();

//        if (!swaggerSettings?.Enabled ?? false)
//            return;

        var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

        app.UseSwagger(options =>
        {
            options.RouteTemplate = "EventGenerator/{documentname}/EventGenerator.yaml";
        });

        app.UseSwaggerUI(
            options =>
            {
                options.RoutePrefix = "EventGenerator";
                provider.ApiVersionDescriptions.ToList().ForEach(
                    description => options.SwaggerEndpoint($"/EventGenerator/{description.GroupName}/EventGenerator.yaml", description.GroupName.ToUpperInvariant())
                );

                options.DocExpansion(DocExpansion.List);
                options.DefaultModelsExpandDepth(-1);
                options.OAuthAppName(AppTitle);
                
                //options.OAuthClientId(swaggerSettings?.OAuthClientId ?? "");
                //options.OAuthClientSecret(swaggerSettings?.OAuthClientSecret ?? "");
            }
        );
    }
}