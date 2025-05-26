// SwaggerOptionsConfiguration.cs
using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen; // Namespace correto para SwaggerGenOptions
using Microsoft.OpenApi.Models;

namespace Employee.API.Configurations;

public class SwaggerOptionsConfiguration : IConfigureOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider _provider;

    public SwaggerOptionsConfiguration(IApiVersionDescriptionProvider provider)
    {
        _provider = provider;
    }

    public void Configure(SwaggerGenOptions options)
    {
        foreach (var description in _provider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(
                description.GroupName,
                new OpenApiInfo
                {
                    Title = $"Employee WebAPI {description.ApiVersion}",
                    Version = description.ApiVersion.ToString(),
                    Description = $"API Documentation for Employee WebAPI {description.ApiVersion}",
                    Contact = new OpenApiContact
                    {
                        Name = "Kaike Rocha",
                        Email = "kaikeksr@outlook.com"
                    }
                });
        }
    }
}