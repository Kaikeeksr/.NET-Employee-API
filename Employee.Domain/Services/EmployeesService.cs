using Employee.Domain.Interfaces;
using Employee.Domain.Interfaces.Repositories;
using Employee.Domain.Interfaces.Services;
using Employee.Domain.Models.Requests;
using Employee.Domain.Models.Responses;
using Employee.Domain.Utils;
using FluentValidation;

namespace Employee.Domain.Services;

public class EmployeesService : ValidationService, IEmployeesService
{
    private readonly IEmployeesRepository _repository;
    private readonly IValidator<EmployeeRequest.CreateEmployeeRequest> _createRequestValidator;
    private readonly IValidator<EmployeeRequest.UpdateEmployeeRequest> _updateRequestValidator;
    
    public EmployeesService(
        IEmployeesRepository repository,
        IValidator<EmployeeRequest.CreateEmployeeRequest> createRequestValidator,
        IValidator<EmployeeRequest.UpdateEmployeeRequest> updateRequestValidator,
        INotificationService notificationService) : base(notificationService)
    {
        _repository = repository;
        _createRequestValidator = createRequestValidator;
        _updateRequestValidator = updateRequestValidator;
    }

    public async Task<List<EmployeeResponse.GetEmployeeResponse>> GetAllEmployees()
    {
        var employeesList = await _repository.GetAllAsync();

        if (employeesList.Count == 0) AddMessage("None employee was found");

        return employeesList;
    }

    public async Task<List<EmployeeResponse.GetEmployeeResponse?>> GetAllEmployeesByDepartment(int departmentId)
    {
        var res = await _repository.GetAllEmployeesByDepartmentAsync(departmentId);

        if (res.Count >= 1) return res;
        
        AddMessage("None employee was found for the department");
        return res;
    }

    public async Task<EmployeeResponse.GetEmployeeResponse?> GetOneEmployeeById(int id)
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
        if (!await ExecuteValidationsAsync(_createRequestValidator, request)) return null;
        
        var res = await _repository.CreateEmployeeAsync(request);

        if (!res.EmployeeAlreadyExists) return res;
        
        var message = $"Employee with cpf {request.Cpf} is already registered";
        AddMessage(message);
        
        return null;
    }

    public async Task<EmployeeResponse.UpdateEmployeeResponse?> UpdateEmployeeAsync(int id,
        EmployeeRequest.UpdateEmployeeRequest request)
    {
        if (!await ExecuteValidationsAsync(_updateRequestValidator, request)) return null;
        
        var res = await _repository.UpdateEmployeeAsync(id, request);
        
        if (res == null) AddMessage($"Employee with id {id} not found for update.");
        
        return res;
    }
}