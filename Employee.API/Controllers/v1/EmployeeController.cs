using Asp.Versioning;
using Employee.Domain;
using Employee.Domain.Interfaces;
using Employee.Domain.Interfaces.Services;
using Employee.Domain.Models.Requests;
using Employee.Domain.Models.Responses;
using Employee.Domain.Utils;
using Microsoft.AspNetCore.Mvc;

namespace Employee.API.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
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
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Route("v{version:apiVersion}/get-all-employees")]
    public async Task<IActionResult> GetAllAsync()
    {
        var employeeList = await _service.GetAllEmployees();
        return CustomResponse(employeeList);
    }

    [HttpPost]
    [ProducesResponseType(typeof(EmployeeResponse.CreateEmployeeResponse), StatusCodes.Status200OK)]
    [Route("v{version:apiVersion}/create-employee")]
    public async Task<IActionResult> CreateEmployee(EmployeeRequest.CreateEmployeeRequest employee)
    {
        var employeeResponse = await _service.CreateEmployeeAsync(employee);
        return CustomResponse(employeeResponse);   
    }
    
    [HttpPut]
    [ProducesResponseType(typeof(EmployeeResponse.DeactivateEmployeeResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Route("v{version:apiVersion}/deactivate-employee/{id}")]
    public async Task<IActionResult> DeactivateEmployee(int id)
    {
        var employee = await _service.DeactivateEmployeeAsync(id);
        return CustomResponse(employee);
    }
    
    [HttpPut()]
    [ProducesResponseType(typeof(EmployeeResponse.ActivateEmployeeResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Route("v{version:apiVersion}/activate-employee/{id}")]
    public async Task<IActionResult> ActivateEmployee(int id)
    {
        var employee = await _service.ActivateEmployeeAsync(id);
        return CustomResponse(employee);
    }
}