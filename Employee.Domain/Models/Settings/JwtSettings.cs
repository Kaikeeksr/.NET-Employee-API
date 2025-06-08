namespace Employee.Domain.Models.Settings;

public class JwtSettings
{
    public string Secret        { get; set; } = null!;
    public string Issuer        { get; set; } = null!;
    public string Audience      { get; set; } = null!;
    public int    ExpireMinutes { get; set; }
}
