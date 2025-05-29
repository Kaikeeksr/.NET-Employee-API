using Employee.Domain.Models.Responses;

namespace Employee.Domain.Interfaces.Repositories;

public interface IEmployeesRepository
{
    Task<List<TblEmployees>> GetAllAsync();
    Task<EmployeeResponse.DisableEmployeeResponse> DisableEmployeeAsync(int id);
}