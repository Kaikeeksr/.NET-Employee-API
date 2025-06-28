using Employee.Domain;
using Employee.Domain.Global.Values;
using Employee.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Employee.Infrastructure.EF.Repositories;

public class DepartmentsRepository : IDepartmentsRepository
{
    private readonly CleverCloudDbContext _context;

    public DepartmentsRepository(CleverCloudDbContext context)
    {
        _context = context;
    }
    
    public async Task<List<TblDepartments>> GetAllDepartmentsAsync()
    {
        if (ValidDepartments.IsUpToDate && ValidDepartments.Departments.Count > 0)
        {
            return ValidDepartments.Departments
                .Select(kv => new TblDepartments {
                    Id         = kv.Key,
                    Department = kv.Value
                })
                .ToList();
        }
        
        var fromDb = await _context.TblDepartments
            .Where(d => d.IsActive)
            .OrderBy(d => d.Id)
            .AsNoTracking()
            .ToListAsync();
        
        ValidDepartments.Departments = fromDb
            .ToDictionary(d => d.Id, d => d.Department);
        ValidDepartments.IsUpToDate = true;

        return fromDb;
    }
}