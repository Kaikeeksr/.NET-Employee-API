using Employee.Domain.Models.Responses;
using Employee.Domain.Models.Requests;

namespace Employee.Domain.Interfaces.Services;

public interface IEmployeesService
{
    Task<List<TblEmployees>> GetAllEmployees();
   //Task<List<TblEmployees>> GetAllEmployeesByDepartment(string department);
    Task<TblEmployees?> GetOneEmployeeById(int id);
    Task<EmployeeResponse.DeactivateEmployeeResponse?> DeactivateEmployeeAsync(int id);
    Task<EmployeeResponse.ActivateEmployeeResponse?> ActivateEmployeeAsync(int id);
    Task<EmployeeResponse.CreateEmployeeResponse?> CreateEmployeeAsync(EmployeeRequest.CreateEmployeeRequest employee);

    Task<EmployeeResponse.UpdateEmployeeResponse?> UpdateEmployeeAsync(int id,
        EmployeeRequest.UpdateEmployeeRequest employee);
}
