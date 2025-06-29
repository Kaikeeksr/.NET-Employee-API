using Asp.Versioning;
using Employee.Domain;
using Employee.Domain.Interfaces;
using Employee.Domain.Interfaces.Services;
using Employee.Domain.Utils;
using Microsoft.AspNetCore.Mvc;
using Employee.Domain.Models.Responses;

namespace Employee.API.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/departments")]
public class DepartmentController : MainController
{
    private readonly IDepartmentsService _service;

    public DepartmentController(
        IDepartmentsService service,
        INotificationService notificationService) : base(notificationService)
    {
        _service = service;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<TblDepartments>), statusCode: StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetDepartments()
    {
        var departmentList = await _service.GetAllDepartments();
        return CustomResponse(departmentList);
    }

    [HttpPatch("{id}/activate")]
    [ProducesResponseType(typeof(DepartmentResponse.ActivateDepartment), statusCode: StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> ActivateDepartmentAsync(int id)
    {
        var res = await _service.SetDepartmentActive(id);
        return CustomResponse(res);
    }

    [HttpPatch("{id}/deactivate")]
    [ProducesResponseType(typeof(DepartmentResponse.DeactivateDepartment), statusCode: StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeactivateDepartmentAsync(int id)
    {
        var res = await _service.SetDepartmentInactive(id);
        return CustomResponse(res);
    }
}