using Asp.Versioning;
using Employee.Domain;
using Employee.Domain.Interfaces;
using Employee.Domain.Interfaces.Services;
using Employee.Domain.Utils;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.Swagger.Annotations;

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
}