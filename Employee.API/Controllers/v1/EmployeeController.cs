using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace Employee.API.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/teste")]
public class EmployeeController : ControllerBase
{
    [HttpGet]
    public IActionResult Get()
    {
        return Ok("Ok");
    }
}