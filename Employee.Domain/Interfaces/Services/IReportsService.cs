using Employee.Domain.Models.Responses;

namespace Employee.Domain.Interfaces.Services;

public interface IReportsService
{
    Task<ReportsResponse.DepartmentSummary> GenerateDepartmentSummaryReport(int departmentId);
    Task<string> GenerateAllDepartmentsSummaryReport();
}