using Asp.Versioning;
using Asp.Versioning.ApiExplorer;

namespace Employee.API.Configurations;

public static class WebApiConfiguration
{
    public static IServiceCollection AddWebApiConfiguration(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddControllers();

        services.AddApiVersioning(options =>
            {
                options.ReportApiVersions = true;
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ApiVersionReader = new UrlSegmentApiVersionReader();
            })
            .AddApiExplorer(options =>
            {
                options.GroupNameFormat = "'v'VVV";
                options.SubstituteApiVersionInUrl = true;
            });

        return services;
    }

    public static IApplicationBuilder UseWebApiConfiguration(this IApplicationBuilder app, IConfiguration configuration,
        IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
    {
        app.UseResponseCompression();
        app.UseResponseCaching();
        app.UseHttpsRedirection();
        app.UseRouting();

        return app;
    }
}