using Asp.Versioning;
using Employee.Domain;
using Employee.Domain.Interfaces;
using Employee.Domain.Interfaces.Services;
using Employee.Domain.Utils;
using Microsoft.AspNetCore.Mvc;

namespace Employee.API.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/get-all-employees")]
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
    public async Task<IActionResult> GetAllAsync()
    {
        var employeeList = await _service.GetAllEmployees();
        return CustomResponse(employeeList);
    }
}