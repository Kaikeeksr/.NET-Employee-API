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

    public async Task<EmployeeResponse.DeactivateEmployeeResponse> DeactivateEmployeeAsync(int id)
    {
        var res = await _repository.SetEmployeeInactiveAsync(id);

        var message = res switch
        {
            null => $"No employee was found for the id {id}",
            { AlreadyInactive: true } => $"Employee with id {id} is already inactive",
            _ => $"Employee with id {id} has been deactivated successfully"
        };

        AddMessage(message);
        return res;
    }

    public async Task<EmployeeResponse.ActivateEmployeeResponse> ActivateEmployeeAsync(int id)
    {
        var res = await _repository.SetEmployeeActiveAsync(id);
        
        var message = res switch
        {
            null => $"No employee was found for the id {id}",
            { AlreadyActive: true } => $"Employee with id {id} is already active",
            _ => $"Employee with id {id} has been activated successfully"
        };
        
        AddMessage(message);
        return res;
    }

    public async Task<EmployeeResponse.CreateEmployeeResponse> CreateEmployeeAsync(EmployeeRequest.CreateEmployeeRequest request)
    {
        if (!ExecuteValidations(new CreateEmployeeRequestValidator(), request)) return null;
        
        var res = await _repository.CreateEmployeeAsync(request);
        return res;
    }
}
