namespace Employee.Domain.Models.Responses;

public class AdminResponse
{
    public class CreateAdminResponse
    {
        public string? Username { get; set; }
        public string? Email { get; set; }
        public DateTime CreatedAt { get; set; }
        public string?  Message { get; set; }
    }
}