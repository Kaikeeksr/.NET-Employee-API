using Asp.Versioning;
using Employee.Domain.Interfaces;
using Employee.Domain.Interfaces.Services;
using Employee.Domain.Models.Requests;
using Employee.Domain.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
namespace Employee.API.Controllers.v1;

[ApiController]
[ApiVersion("1.0")]
[Route("v{version:apiVersion}/admin")]
public class AdminController : MainController 
{
    private readonly IAuthenticationService _authenticationservice;
    private readonly IAdminService _service;

    public AdminController(
        INotificationService notificationService,
        IAdminService service,
        IAuthenticationService authenticationservice) : base(notificationService)
    {
        _service = service;
        _authenticationservice = authenticationservice;
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("create")]
    public async Task<IActionResult> CreateAdmin(AdminRequest.CreateAdminRequest request)
    {
        var userCreated = await _service.CreateAdminAsync(request);
        
        return CustomResponse(userCreated);
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Login(AdminRequest.LoginRequest loginRequest)
    {
        var token = await _authenticationservice.LoginAsync(loginRequest);
        
        return CustomResponse(token);
    }
}