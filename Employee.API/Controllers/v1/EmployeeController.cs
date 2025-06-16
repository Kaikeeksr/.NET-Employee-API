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
    public enum  Department
    {
        Design,
        Support,
        Legal,
        Marketing,
        IT,
        Logistics,
        Accounting
    }
    
    public EmployeeController(
        INotificationService notificationService,
        IEmployeesService service) : base(notificationService) 
    { 
        _service = service;    
    }
    
    [HttpGet]
    [SwaggerOperation(Tags = new[] {"GET"})]
    [ProducesResponseType(typeof(List<TblEmployees>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAllAsync()
    {
        var employeeList = await _service.GetAllEmployees();
        return CustomResponse(employeeList);
    }

    /*[HttpGet("department/{department}")]
    [SwaggerOperation(Tags = new[] { "GET" })]
    [ProducesResponseType(typeof(List<TblEmployees>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetEmployeeByDepartment(Department department)
    {
        var employeeList = await _service.GetAllEmployeesByDepartment(department.ToString());
        return CustomResponse(employeeList);
    }*/

    [HttpGet("{id}")]
    [SwaggerOperation(Tags = new[] {"GET"})]
    [ProducesResponseType(typeof(TblEmployees), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var employee = await _service.GetOneEmployeeById(id);
        return CustomResponse(employee);
    }

    [HttpPost]
    [SwaggerOperation(Tags = new[] {"POST"})]
    [ProducesResponseType(typeof(EmployeeResponse.CreateEmployeeResponse), StatusCodes.Status201Created)]
    public async Task<IActionResult> CreateEmployee(EmployeeRequest.CreateEmployeeRequest employee)
    {
        var employeeResponse = await _service.CreateEmployeeAsync(employee);
        return CustomResponse(employeeResponse);   
    }

    [HttpPatch("{id}"),
    SwaggerOperation(Tags = new[] {"PATCH"}),
    ProducesResponseType(typeof(EmployeeResponse.UpdateEmployeeResponse), StatusCodes.Status201Created)]
    public async Task<IActionResult> UpdateEmployee(int id, EmployeeRequest.UpdateEmployeeRequest employee)
    {
        var employeeResponse = await _service.UpdateEmployeeAsync(id, employee);
        return CustomResponse(employeeResponse);   
    }
    
    [HttpPut("{id}/deactivate")]
    [SwaggerOperation(Tags = new[] {"PUT"})]
    [ProducesResponseType(typeof(EmployeeResponse.DeactivateEmployeeResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeactivateEmployee(int id)
    {
        var employee = await _service.DeactivateEmployeeAsync(id);
        return CustomResponse(employee);
    }
    
    [HttpPut("{id}/activate")]
    [SwaggerOperation(Tags = new[] {"PUT"})]
    [ProducesResponseType(typeof(EmployeeResponse.ActivateEmployeeResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ActivateEmployee(int id)
    {
        var employee = await _service.ActivateEmployeeAsync(id);
        return CustomResponse(employee);
    }
}