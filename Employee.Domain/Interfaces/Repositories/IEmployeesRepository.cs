using Employee.Domain.Models.Responses;
using Employee.Domain.Models.Requests;

namespace Employee.Domain.Interfaces.Repositories;

public interface IEmployeesRepository
{
    Task<List<TblEmployees>> GetAllAsync();
    Task<EmployeeResponse.DeactivateEmployeeResponse> SetEmployeeInactiveAsync(int id);
    Task<EmployeeResponse.ActivateEmployeeResponse> SetEmployeeActiveAsync(int id);
    Task<EmployeeResponse.CreateEmployeeResponse> CreateEmployeeAsync(EmployeeRequest.CreateEmployeeRequest request);
}