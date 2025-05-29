using AutoMapper;
using Employee.Domain;
using Employee.Domain.Interfaces.Repositories;
using Employee.Domain.Models.Responses;
using Employee.Domain.Models.Requests;
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

    public async Task<EmployeeResponse.DeactivateEmployeeResponse> SetEmployeeInactiveAsync(int id)
    {
        var employee = await _context.TblEmployees.FindAsync(id);
        if (employee == null) return null;
        
        var isAlreadyInactive = employee.EStatus == "Z";
        if (!isAlreadyInactive)
        {
            employee.EStatus = "Z";
            await _context.SaveChangesAsync();
        }

        var response = new EmployeeResponse.DeactivateEmployeeResponse();
        response.AlreadyInactive = isAlreadyInactive;

        return response;
    }

    public async Task<EmployeeResponse.ActivateEmployeeResponse> SetEmployeeActiveAsync(int id)
    {
        var employee = await _context.TblEmployees.FindAsync(id);
        if(employee == null) return null;
        
        var isAlreadyActive = employee.EStatus == "A";
        if(!isAlreadyActive)
        {
            employee.EStatus = "A";
            await _context.SaveChangesAsync();
        }
        
        var response = new EmployeeResponse.ActivateEmployeeResponse();
        response.AlreadyActive = isAlreadyActive;
        
        return response;
    }

    public async Task<EmployeeResponse.CreateEmployeeResponse> CreateEmployeeAsync(EmployeeRequest.CreateEmployeeRequest employee)
    {
        var employeeExists = await _context.TblEmployees.AnyAsync(e => e.ECpf == employee.ECpf);

        if (employeeExists)
        {
            return new EmployeeResponse.CreateEmployeeResponse
            {
                EmployeeAlreadyExists = true
            };
        }
        
        var employeeEntity = _mapper.Map<TblEmployees>(employee);
        employeeEntity.EStatus = "A"; 
        employeeEntity.CreatedAt = DateTime.Now;
        
        await _context.TblEmployees.AddAsync(employeeEntity);
        await _context.SaveChangesAsync();
        
        return _mapper.Map<EmployeeResponse.CreateEmployeeResponse>(employeeEntity);
    }
}