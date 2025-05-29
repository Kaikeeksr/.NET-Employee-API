namespace Employee.Domain.Models.Responses;
public class EmployeeResponse
{
    public class DisableEmployeeResponse
    {
        public int EId { get; set; }
        public char EStatus { get; set; }
        public bool AlreadyInactive { get; set; }
    }
}