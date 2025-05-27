using Microsoft.Extensions.Configuration;

public class ConfigurationLoader
{
    public IConfiguration Configuration { get; }

    public ConfigurationLoader()
    {
        var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("../Employee.API/appsettings.json", optional: false, reloadOnChange: true);
        Configuration = builder.Build();
    }
}