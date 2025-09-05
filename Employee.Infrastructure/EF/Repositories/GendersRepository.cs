using Employee.Domain;
using Employee.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Employee.Infrastructure.EF.Repositories;

public class GendersRepository : IGendersRepository
{
    private readonly CleverCloudDbContext _context;

    public GendersRepository(CleverCloudDbContext context)
    {
        _context = context;
    }
    public async Task<List<TblGenders>> GetAllGendersAsync()
    {
        return await _context.TblGenders
            .Where(g => g.IsActive == true)
            .OrderBy(g => g.Id)
            .AsNoTracking()
            .ToListAsync();
    }
}