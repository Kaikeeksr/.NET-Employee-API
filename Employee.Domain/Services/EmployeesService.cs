using Employee.Domain.Interfaces;
using Employee.Domain.Interfaces.Repositories;
using Employee.Domain.Interfaces.Services;
using Employee.Domain.Models.Responses;
using Employee.Domain.Models.Requests;
using Employee.Domain.Utils;

namespace Employee.Domain.Services;

public class EmployeesService : ValidationService, IEmployeesService
{
    private readonly IEmployeesRepository _repository;
    
    // array fixo provisório
    private readonly string[] _departmentList = {"Design", "Support", "Legal", "Marketing", "IT", "Logistics", "Accounting"};
    public EmployeesService(
        IEmployeesRepository repository,
        INotificationService notificationService) : base(notificationService)
    {
        _repository = repository;
    }

    public async Task<List<TblEmployees>> GetAllEmployees()
    {
        var employeesList = await _repository.GetAllAsync();

        if (employeesList.Count == 0) AddMessage($"None employee was found");

        return employeesList;
    }

   /* public async Task<List<TblEmployees>> GetAllEmployeesByDepartment(string department)
    {
        if (!_departmentList.Contains(department))
        {
            AddMessage("Invalid department");
            return null;
        }
        
        return await _repository.GetAllEmployeesByDepartmentAsync(department);
    }*/

    public async Task<TblEmployees?> GetOneEmployeeById(int id)
    {
        var employee = await _repository.GetOneAsync(id);

        if (employee != null) return employee;
        AddMessage($"Employee with the id {id} not found");
        return null;
    }
    
    public async Task<EmployeeResponse.DeactivateEmployeeResponse?> DeactivateEmployeeAsync(int id)
    {
        var res = await _repository.SetEmployeeInactiveAsync(id);

        if (res == null)
        {
            AddMessage($"No employee was found for the id {id}");
            return null;
        }

        if (res.AlreadyInactive)
        {
            AddMessage($"Employee with id {id} is already inactive");
            return null;
        }
        
        return res;
    }

    public async Task<EmployeeResponse.ActivateEmployeeResponse?> ActivateEmployeeAsync(int id)
    {
        var res = await _repository.SetEmployeeActiveAsync(id);

        if (res == null)
        {
            AddMessage($"No employee was found for the id {id}");
            return null;
        }

        if (res.AlreadyActive)
        {
            AddMessage($"Employee with id {id} is already active");
            return null;
        }
        
        return res;
    }

    public async Task<EmployeeResponse.CreateEmployeeResponse?> CreateEmployeeAsync(
        EmployeeRequest.CreateEmployeeRequest request)
    {
        if (!ExecuteValidations(new CreateEmployeeRequestValidator(), request)) return null;

        var res = await _repository.CreateEmployeeAsync(request);

        if (!(res.EmployeeAlreadyExists)) return res;
        
        var message = $"Employee with cpf {request.ECpf} is already registered";
        AddMessage(message);
        
        return null;

    }

    public async Task<EmployeeResponse.UpdateEmployeeResponse?> UpdateEmployeeAsync(int id,
        EmployeeRequest.UpdateEmployeeRequest request)
    {
        if(!ExecuteValidations(new UpdateEmployeeRequestValidator(), request)) return null;
        
        var res = await _repository.UpdateEmployeeAsync(id, request);
      
        return res ?? null;
    }
}
