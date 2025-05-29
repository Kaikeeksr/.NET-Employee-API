using AutoMapper;
using Employee.Domain;
using Employee.Domain.Interfaces;
using Employee.Domain.Interfaces.Repositories;
using Employee.Domain.Models.Responses;
using Microsoft.EntityFrameworkCore;

namespace Employee.Infrastructure.EF.Repositories;

public class EmployeesRepository : IEmployeesRepository
{
    private readonly CleverCloudDbContext _context;
    private readonly IMapper _mapper;
    public EmployeesRepository(CleverCloudDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = _mapper;
    }

    public async Task<List<TblEmployees>> GetAllAsync()
    {
        var employeesList = await _context.TblEmployees
            .Where(e => e.EStatus == "A")
            .AsNoTracking()
            .ToListAsync();
                                   
        return employeesList;
    }

    public async Task<EmployeeResponse.DisableEmployeeResponse> DisableEmployeeAsync(int id)
    {
        var employee = await _context.TblEmployees
            .Include(e => e.EStatusNavigation)
            .FirstOrDefaultAsync(e => e.EId == id);
        
        if(employee == null) return null;
        
        employee.EStatus = "Z";
        
        await _context.SaveChangesAsync();

        return _mapper.Map<EmployeeResponse.DisableEmployeeResponse>(employee);
    }
}