namespace Employee.Domain.Models.Responses;

public abstract class DepartmentResponse
{
    public class ActivateDepartment
    {
        public bool AlreadyActive { get; set; }
        public bool IsActive { get; set; }
        public int DepartmentId { get; set; }   
    }
    
    public class DeactivateDepartment
    {
        public bool AlreadyInactive { get; set; }
        public bool IsActive { get; set; }
        public int DepartmentId { get; set; }   
    }
}