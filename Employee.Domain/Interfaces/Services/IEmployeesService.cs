using Employee.Domain.Models.Responses;

namespace Employee.Domain.Interfaces.Services;

public interface IEmployeesService
{
    Task<List<TblEmployees>> GetAllEmployees();
    Task<EmployeeResponse.DisableEmployeeResponse> DisableEmployee(int id);
}
