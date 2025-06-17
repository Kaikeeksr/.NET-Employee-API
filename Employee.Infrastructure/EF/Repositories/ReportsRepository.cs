using AutoMapper;
using Employee.Domain.Interfaces.Repositories;
using Employee.Domain.Models.Responses;
using Microsoft.EntityFrameworkCore;

namespace Employee.Infrastructure.EF.Repositories;

public class ReportsRepository : IReportsRepository
{
    private readonly CleverCloudDbContext _context;
    private readonly IConfigurationProvider _projectMapper;
    private readonly IMapper _mapper;

    public ReportsRepository(
        CleverCloudDbContext context,
        IConfigurationProvider projectMapper,
        IMapper mapper)
    {
        _context = context;
        _projectMapper = projectMapper;
        _mapper = mapper;
    }
    
    public Task<ReportsResponse.DepartmentSummary> GenerateDepartmentSummaryReportAsync(int departmentId)
    {
        var departmentSummary = _context.TblEmployees
            .Where(e => e.DepartmentId == departmentId)
            .GroupBy(e => new { e.DepartmentId, e.EDepartmentNavigation.Department })
            .Select(g => new ReportsResponse.DepartmentSummary()
            {
                DepartmentId = g.Key.DepartmentId,
                Name = g.Key.Department,
                TotalEmployees = g.Count(),
                ActiveEmployees = g.Sum(e => e.EStatus == "A" ? 1 : 0),
                InactiveEmployees = g.Sum(e => e.EStatus == "Z" ? 1 : 0),
                Payroll = (int)(g.Sum(e => (decimal?)e.EWage) ?? 0m)
            })
            .FirstOrDefaultAsync();

        return (departmentSummary ?? null)!;
    }

    public Task<string> GenerateAllDepartmentsSummaryReportAsync()
    {
        throw new NotImplementedException();
    }
}