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

        if (employeesList.Count == 0) AddMessage($"Nenhum usuário foi encontrado");

        return employeesList;
    }

    public Task<EmployeeResponse.DisableEmployeeResponse> DisableEmployee(int id)
    {
        var disableEmployeeResponse = _repository.DisableEmployeeAsync(id);
        return disableEmployeeResponse;
    }
}
