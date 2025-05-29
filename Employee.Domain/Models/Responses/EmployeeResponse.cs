namespace Employee.Domain.Models.Responses;
public class EmployeeResponse
{
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