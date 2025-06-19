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
            .GroupBy(e => new { e.DepartmentId, e.DepartmentNavigation.Department })
            .Select(g => new ReportsResponse.DepartmentSummary()
            {
                DepartmentId = g.Key.DepartmentId,
                Name = g.Key.Department,
                TotalEmployees = g.Count(),
                ActiveEmployees = g.Sum(e => e.Status == "A" ? 1 : 0),
                InactiveEmployees = g.Sum(e => e.Status == "Z" ? 1 : 0),
                Payroll = (int)(g.Sum(e => (decimal?)e.Wage) ?? 0m)
            })
            .FirstOrDefaultAsync();

        return (departmentSummary ?? null)!;
    }

    public async Task<ReportsResponse.AllDepartmentsSummary> GenerateAllDepartmentsSummaryReportAsync()
    {
        var result = await _context.TblEmployees
            .GroupBy(e => new { e.DepartmentId, e.DepartmentNavigation.Department })
            .Select(g => new
            {
                Department = new ReportsResponse.DepartmentSummary()
                {
                    DepartmentId = g.Key.DepartmentId,
                    Name = g.Key.Department,
                    TotalEmployees = g.Count(),
                    ActiveEmployees = g.Count(e => e.Status == "A"),
                    InactiveEmployees = g.Count(e => e.Status == "Z"),
                    Payroll = (int)(g.Sum(e => (decimal?)e.Wage) ?? 0m)
                },
                Totals = new
                {
                    TotalEmployees = g.Count(),
                    ActiveEmployees = g.Count(e => e.Status == "A"),
                    InactiveEmployees = g.Count(e => e.Status == "Z"),
                    Payroll = g.Sum(e => (decimal?)e.Wage) ?? 0m
                }
            })
            .ToListAsync();
        
        var summary = new ReportsResponse.AllDepartmentsSummary()
        {
            TotalEmployees = result.Sum(x => x.Totals.TotalEmployees),
            ActiveEmployees = result.Sum(x => x.Totals.ActiveEmployees),
            InactiveEmployees = result.Sum(x => x.Totals.InactiveEmployees),
            TotalPayroll = (float)result.Sum(x => x.Totals.Payroll),
            Departments = result.Select(x => x.Department).ToList()
        };

        return summary;
    }
}