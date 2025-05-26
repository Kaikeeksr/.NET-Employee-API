using System.IO.Compression;
using Employee.API.Configurations;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.ResponseCompression;

namespace Employee.API;

public class Startup
{
    public IConfiguration Configuration { get; }
    public IWebHostEnvironment Env { get; }

    public Startup(IConfiguration configuration, IWebHostEnvironment env)
    {
        Configuration = configuration;
        Env = env;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        //add o banco de dados

        services.Configure<GzipCompressionProviderOptions>(options => { options.Level = CompressionLevel.Optimal; });

        services.AddResponseCompression(options =>
        {
            options.EnableForHttps = true;
            options.Providers.Add<GzipCompressionProvider>();
        });

        services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy",
                builder => { builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader(); });
        });
        
        services.AddControllers();
        services.AddWebApiConfiguration(Configuration);
        services.AddSwaggerConfiguration();
        services.AddEndpointsApiExplorer();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("CorsPolicy");
            app.UseResponseCompression();
            app.UseSwaggerUI();
            app.UseSwagger();
            
        }
}