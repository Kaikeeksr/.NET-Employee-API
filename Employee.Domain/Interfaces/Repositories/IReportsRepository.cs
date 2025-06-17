using Employee.Domain.Models.Responses;

namespace Employee.Domain.Interfaces.Repositories;

public interface IReportsRepository
{
    Task<ReportsResponse.DepartmentSummary> GenerateDepartmentSummaryReportAsync(int departmentId);
    Task<ReportsResponse.AllDepartmentsSummary> GenerateAllDepartmentsSummaryReportAsync();
}