using Asp.Versioning;
using Employee.Domain.Interfaces;
using Employee.Domain.Interfaces.Services;
using Employee.Domain.Utils;
using Microsoft.AspNetCore.Mvc;

namespace Employee.API.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/reports")]
public class ReportsController : MainController
{
    private readonly IReportsService _service;
    
    public ReportsController(
        IReportsService service,
        INotificationService notificationService) : base(notificationService)
    {
        _service = service;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetSummary(int deparmentId)
    {
        var res = await _service.GenerateDepartmentSummaryReport(deparmentId);
        return CustomResponse(res);
    }
}