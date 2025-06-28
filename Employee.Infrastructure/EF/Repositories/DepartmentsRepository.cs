using Employee.Domain;
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
        return await _context.TblDepartments
            .Where(d => d.IsActive)
            .OrderBy(d => d.Id)
            .AsNoTracking()
            .ToListAsync();
    }
}