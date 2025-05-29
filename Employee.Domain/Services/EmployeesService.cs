using Employee.Domain.Interfaces;
using Employee.Domain.Interfaces.Repositories;
using Employee.Domain.Interfaces.Services;
using Employee.Domain.Models.Responses;
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

    public async Task<EmployeeResponse.DisableEmployeeResponse> DeactivateEmployeeAsync(int id)
    {
        var resp = await _repository.SetEmployeeInactiveAsync(id);

        var message = resp switch
        {
            null => $"No employee was found for the id {id}",
            { AlreadyInactive: true } => $"Employee with id {id} is already inactive",
            _ => $"Employee with id {id} has been deactivated successfully"
        };

        AddMessage(message);
        return resp;
    }
}
