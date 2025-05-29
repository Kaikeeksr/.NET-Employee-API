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
        _mapper = mapper;
    }

    public async Task<List<TblEmployees>> GetAllAsync()
    {
        var employeesList = await _context.TblEmployees
            .Where(e => e.EStatus == "A")
            .AsNoTracking()
            .ToListAsync();
                                   
        return employeesList;
    }

    public async Task<EmployeeResponse.DisableEmployeeResponse> SetEmployeeInactiveAsync(int id)
    {
        var employee = await _context.TblEmployees.FindAsync(id);
        if (employee == null) return null;
        
        var wasAlreadyInactive = employee.EStatus == "Z";
        if (!wasAlreadyInactive)
        {
            employee.EStatus = "Z";
            await _context.SaveChangesAsync();
        }

        var response = new EmployeeResponse.DisableEmployeeResponse();
        response.AlreadyInactive = wasAlreadyInactive;

        return response;
    }
}