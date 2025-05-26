namespace Employee.API.Configurations;

public static class DIConfiguration
{
    public static IServiceCollection AddDependencyInjectionConfiguration(this IServiceCollection services)
    {
        services.AddHttpContextAccessor();
        services.AddMvcCore().AddApiExplorer();
        
        return services;
    }
}