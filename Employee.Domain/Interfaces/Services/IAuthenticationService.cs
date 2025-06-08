namespace Employee.Domain.Interfaces.Services;

public interface IAuthenticationService
{
    Task<string?> LoginAsync(Models.Requests.AdminRequest.LoginRequest request);
}