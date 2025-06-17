using Employee.Domain.Interfaces;
using Employee.Domain.Interfaces.Repositories;
using Employee.Domain.Interfaces.Services;
using Employee.Domain.Models.Responses;
using Employee.Domain.Utils;

namespace Employee.Domain.Services;

public class ReportsService : ValidationService, IReportsService
{
    private readonly IReportsRepository _repository;
    
    public ReportsService(
        IReportsRepository repository,
        INotificationService notificationService) : base(notificationService)
    {
        _repository = repository;
    }

    public Task<ReportsResponse.DepartmentSummary> GenerateDepartmentSummaryReport(int departmentId)
    {
        var res = _repository.GenerateDepartmentSummaryReportAsync(departmentId);

        if (res == null)
        {
            AddMessage("Department Summary Report is empty");
            return null;
        }

        return res;
    }

    public Task<string> GenerateAllDepartmentsSummaryReport()
    {
        throw new NotImplementedException();
    }
}