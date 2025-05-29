using System.IO.Compression;
using dotenv.net;
using Employee.API.Configurations;
using Employee.Infrastructure.Configuration;
using Employee.Infrastructure.EF;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;

namespace Employee.API;

public class Startup
{
    public IConfiguration Configuration { get; }
    public IWebHostEnvironment Env { get; }

    public Startup(IConfiguration configuration, IWebHostEnvironment env)
    {
        DotEnv.Load();
        Configuration = configuration;
        Env = env;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING")
                               ?? Configuration.GetConnectionString("CleverCloud");
        
        services.AddDbContext<CleverCloudDbContext>(options =>
        {
            options.UseMySQL(connectionString);
        });

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

        services.AddDependencyInjectionConfiguration();
        services.AddControllers();
        services.AddWebApiConfiguration(Configuration);
        services.AddSwaggerConfiguration();
        services.AddEndpointsApiExplorer();
        services.AddAutoMapper(typeof(AutoMapperConfig));
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("CorsPolicy");
            app.UseResponseCompression();
            app.UseRouting();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Employee API V1");
                c.RoutePrefix = string.Empty;
            });
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
}