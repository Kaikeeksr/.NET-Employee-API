using Employee.Domain.Interfaces.Repositories;
using Employee.Domain.Global.Values;

namespace Employee.API.Configurations;

public class SetGlobalValues
{
    public static async Task PopulateValidDepartments(IServiceProvider service)
    {
        var departmentRepository = service.GetRequiredService<IDepartmentsRepository>();
        var departments = await departmentRepository.GetAllDepartmentsAsync();

        ValidDepartments.Departments = departments
            .ToDictionary(department => department.Id, department => department.Department);
    }
}