using Employee.Domain;
using Employee.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Employee.Infrastructure.EF.Repositories;

public class EmployeesRepository : IEmployeesRepository
{
    private readonly CleverCloudDbContext _context;

    public EmployeesRepository(CleverCloudDbContext context)
    {
        _context = context;
    }

    public async Task<List<TblEmployees>> GetAllAsync()
    {
        var employeesList = await _context.TblEmployees
                                   .AsNoTracking()
                                   .ToListAsync();
        
        return employeesList;
    }
}