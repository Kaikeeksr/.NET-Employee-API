using Asp.Versioning;
using Employee.Domain;
using Employee.Domain.Interfaces;
using Employee.Domain.Interfaces.Services;
using Employee.Domain.Models.Requests;
using Employee.Domain.Models.Responses;
using Employee.Domain.Utils;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Employee.API.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/employees")]
public class EmployeeController : MainController
{
    private readonly IEmployeesService _service;
    
    public EmployeeController(
        INotificationService notificationService,
        IEmployeesService service) : base(notificationService) 
    { 
        _service = service;    
    }
    
    [HttpGet]
    [ProducesResponseType(typeof(List<TblEmployees>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllAsync()
    {
        var employeeList = await _service.GetAllEmployees();
        return CustomResponse(employeeList);
    }

    [HttpGet("by-department/{departmentId}")]
    [ProducesResponseType(typeof(List<TblEmployees>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetEmployeeByDepartment(int departmentId)
    {
        var employeeList = await _service.GetAllEmployeesByDepartment(departmentId);
        return CustomResponse(employeeList);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(TblEmployees), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var employee = await _service.GetOneEmployeeById(id);
        return CustomResponse(employee);
    }

    [HttpPost]
    [ProducesResponseType(typeof(EmployeeResponse.CreateEmployeeResponse), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateEmployee(EmployeeRequest.CreateEmployeeRequest employee)
    {
        var employeeResponse = await _service.CreateEmployeeAsync(employee);
        return CustomResponse(employeeResponse);   
    }

    [HttpPatch("{id}"),
    ProducesResponseType(typeof(EmployeeResponse.UpdateEmployeeResponse), StatusCodes.Status201Created)]
    public async Task<IActionResult> UpdateEmployee(int id, EmployeeRequest.UpdateEmployeeRequest employee)
    {
        var employeeResponse = await _service.UpdateEmployeeAsync(id, employee);
        return CustomResponse(employeeResponse);   
    }
    
    [HttpPut("{id}/deactivate")]
    [ProducesResponseType(typeof(EmployeeResponse.DeactivateEmployeeResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeactivateEmployee(int id)
    {
        var employee = await _service.DeactivateEmployeeAsync(id);
        return CustomResponse(employee);
    }
    
    [HttpPut("{id}/activate")]
    [ProducesResponseType(typeof(EmployeeResponse.ActivateEmployeeResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ActivateEmployee(int id)
    {
        var employee = await _service.ActivateEmployeeAsync(id);
        return CustomResponse(employee);
    }
}