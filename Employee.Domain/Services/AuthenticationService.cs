using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Employee.Domain.Interfaces;
using Employee.Domain.Interfaces.Services;
using Employee.Domain.Models.Requests;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Employee.Domain.Utils;
using Employee.Domain.Models.Settings;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Employee.Domain.Services;

public class AuthenticationService : ValidationService, IAuthenticationService
{
    private readonly UserManager<IdentityUser> _userManager;

    public AuthenticationService(
        UserManager<IdentityUser> userManager,
        INotificationService notificationService) : base(notificationService)
    {
        _userManager = userManager;
    }

    public async Task<string?> LoginAsync(AdminRequest.LoginRequest request)
    {
        var user = await _userManager.FindByNameAsync(request.Username);
      
        if (user == null || !await _userManager.CheckPasswordAsync(user, request.Password))
        {
            AddMessage("Invalid username or password");
            return null;
        }

        return await GenerateToken(user);
    }

    private async Task<string?> GenerateToken(IdentityUser user)
    {
        var jwtSettings = new JwtSettings()
        {
            Secret = Environment.GetEnvironmentVariable("JWTSETTINGS__SECRET")!,
            Issuer = Environment.GetEnvironmentVariable("JWTSETTINGS__ISSUER")!,
            Audience = Environment.GetEnvironmentVariable("JWTSETTINGS__AUDIENCE")!,
            ExpireMinutes = int.Parse(Environment.GetEnvironmentVariable("JWTSETTINGS__EXPIREMINUTES")!)
        };

        var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName!),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };
        
        var roles = await _userManager.GetRolesAsync(user);
        foreach (var role in roles)
        {
            claims.Add(new Claim(JwtRegisteredClaimNames.Actort, role));
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: jwtSettings.Issuer,
            audience: jwtSettings.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(jwtSettings.ExpireMinutes),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
