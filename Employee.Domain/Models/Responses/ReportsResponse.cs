namespace Employee.Domain.Models.Responses;

public class ReportsResponse
{
    public class DepartmentSummary
    {
        public required int? DepartmentId { get; set; }
        public required string Name { get; set; }
        public required int TotalEmployees {get; set; }
        public required int ActiveEmployees {get; set; }
        public required int InactiveEmployees {get; set; }
        public required int Payroll {get; set; }
    }
}