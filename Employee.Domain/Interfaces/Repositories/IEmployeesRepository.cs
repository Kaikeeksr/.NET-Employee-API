using Employee.Domain.Models.Responses;
using Employee.Domain.Models.Requests;

namespace Employee.Domain.Interfaces.Repositories;

public interface IEmployeesRepository
{
    Task<List<EmployeeResponse.GetEmployeeResponse>> GetAllAsync();
    Task<EmployeeResponse.GetEmployeeResponse?> GetOneAsync(int id);
    //Task<List<TblEmployees>> GetAllEmployeesByDepartmentAsync(string department);
    Task<EmployeeResponse.DeactivateEmployeeResponse?> SetEmployeeInactiveAsync(int id);
    Task<EmployeeResponse.ActivateEmployeeResponse?> SetEmployeeActiveAsync(int id);
    Task<EmployeeResponse.CreateEmployeeResponse?> CreateEmployeeAsync(EmployeeRequest.CreateEmployeeRequest request);
    Task<EmployeeResponse.UpdateEmployeeResponse?> UpdateEmployeeAsync(int id, EmployeeRequest.UpdateEmployeeRequest request);
}