namespace Employee.Domain.Models.Responses;
public class EmployeeResponse
{
    public class GetEmployeeResponse
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string Cpf { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string? Telephone { get; set; }
        
        public required decimal Wage { get; set; }

        public required DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
        public required string Department { get; set; }
        public required string Gender { get; set; }
    }

    public class DeactivateEmployeeResponse
    {
        public int Id { get; set; }
        public string? Status { get; set; }
        public bool AlreadyInactive { get; set; }
    }
    
    public class ActivateEmployeeResponse
    {
        public int Id { get; set; }
        public string? Status { get; set; }
        public bool AlreadyActive { get; set; }
    }
    
    public class CreateEmployeeResponse
    {
        public int? id { get; set; }
        public string? Name { get; set; }
        public bool EmployeeAlreadyExists { get; set; } = false;
    }

    public class UpdateEmployeeResponse
    {
        public int Id { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}