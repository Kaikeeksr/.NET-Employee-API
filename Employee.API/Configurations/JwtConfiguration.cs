using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Employee.Domain.Models.Settings;

namespace Employee.API.Configurations
{
    public static class JwtConfiguration
    {
        public static IServiceCollection AddJwtConfiguration(this IServiceCollection services)
        {
            var jwt = LoadFromEnvironment();
            var key = Encoding.UTF8.GetBytes(jwt.Secret);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme    = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata      = true;
                options.SaveToken                 = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer           = true,
                    ValidateAudience         = true,
                    ValidateLifetime         = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer              = jwt.Issuer,
                    ValidAudience            = jwt.Audience,
                    IssuerSigningKey         = new SymmetricSecurityKey(key),
                    
                    // Configuração crítica para reconhecer roles
                    RoleClaimType = ClaimTypes.Role,
                    NameClaimType = ClaimTypes.NameIdentifier
                };
            });

            return services;
        }

        private static JwtSettings LoadFromEnvironment()
        {
            return new JwtSettings
            {
                Secret = Environment.GetEnvironmentVariable("JWTSETTINGS__SECRET") 
                         ?? throw new Exception("JWTSETTINGS__SECRET não encontrado"),
                Issuer = Environment.GetEnvironmentVariable("JWTSETTINGS__ISSUER") 
                         ?? throw new Exception("JWTSETTINGS__ISSUER não encontrado"),
                Audience = Environment.GetEnvironmentVariable("JWTSETTINGS__AUDIENCE") 
                           ?? throw new Exception("JWTSETTINGS__AUDIENCE não encontrado"),
                ExpireMinutes = int.TryParse(Environment.GetEnvironmentVariable("JWTSETTINGS__EXPIREMINUTES"), out var minutes)
                                ? minutes
                                : 60
            };
        } 
    }
}
