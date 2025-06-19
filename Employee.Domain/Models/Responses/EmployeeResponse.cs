namespace Employee.Domain.Models.Responses;
public class EmployeeResponse
{
    public class GetEmployeeResponse
    {
        public int EId { get; set; }

        public string EName { get; set; } = null!;

        public string ECpf { get; set; } = null!;

        public string EEmail { get; set; } = null!;

        public string? ETel { get; set; }
        
        public required decimal EWage { get; set; }

        public required DateTime CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }
        public required string Department { get; set; }
        public required string Gender { get; set; }
    }

    public class DeactivateEmployeeResponse
    {
        public int EId { get; set; }
        public string? EStatus { get; set; }
        public bool AlreadyInactive { get; set; }
    }
    
    public class ActivateEmployeeResponse
    {
        public int EId { get; set; }
        public string? EStatus { get; set; }
        public bool AlreadyActive { get; set; }
    }
    
    public class CreateEmployeeResponse
    {
        public int? Eid { get; set; }
        public string? EName { get; set; }
        public bool EmployeeAlreadyExists { get; set; } = false;
    }

    public class UpdateEmployeeResponse
    {
        public int EId { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}