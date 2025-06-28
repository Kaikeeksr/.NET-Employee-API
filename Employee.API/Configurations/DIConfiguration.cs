using Employee.Domain.Interfaces;
using Employee.Domain.Interfaces.Repositories;
using Employee.Domain.Interfaces.Services;
using Employee.Domain.Models.Requests;
using Employee.Domain.Services;
using Employee.Domain.Utils;
using Employee.Infrastructure.EF.Repositories;

namespace Employee.API.Configurations;

public static class DIConfiguration
{
    public static IServiceCollection AddDependencyInjectionConfiguration(this IServiceCollection services)
    {
        //Configuration
        services.AddHttpContextAccessor();
        services.AddMvcCore().AddApiExplorer();
        services.AddScoped<INotificationService, NotificationService>();

        //Services
        services.AddScoped<IEmployeesService, EmployeesService>();
        services.AddScoped<IAuthenticationService, AuthenticationService>();
        services.AddScoped<IAdminService, AdminService>();
        services.AddScoped<IDepartmentsService, DepartmentsService>();
        services.AddScoped<IReportsService, ReportsService>();
        services.AddScoped<ICachingService, CachingService>();
        
        //Repositories
        services.AddScoped<IEmployeesRepository, EmployeesRepository>();
        services.AddScoped<IDepartmentsRepository, DepartmentsRepository>();
        services.AddScoped<IReportsRepository, ReportsRepository>();
        
        // Validations
        services.AddScoped<CreateEmployeeRequestValidator>();
        services.AddScoped<UpdateEmployeeRequestValidator>();
        
        return services;
    }
}