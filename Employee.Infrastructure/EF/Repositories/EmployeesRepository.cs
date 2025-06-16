using AutoMapper;
using Employee.Domain;
using Employee.Domain.Interfaces.Repositories;
using Employee.Domain.Models.Responses;
using Employee.Domain.Models.Requests;
using Microsoft.EntityFrameworkCore;
using AutoMapper.QueryableExtensions;

namespace Employee.Infrastructure.EF.Repositories;

public class EmployeesRepository : IEmployeesRepository
{
    private readonly CleverCloudDbContext _context;
    private readonly IConfigurationProvider _projectMapper;
    private readonly IMapper _mapper;
    
    public EmployeesRepository(CleverCloudDbContext context, IConfigurationProvider projectMapper, IMapper mapper)
    {
        _context = context;
        _projectMapper = projectMapper;
        _mapper = mapper;
    }

    public async Task<List<EmployeeResponse.GetEmployeeResponse>> GetAllAsync()
    {
        return await _context.TblEmployees
            .Where(e => e.EStatus != "Z")
            .OrderBy(e => e.EName)
            .ProjectTo<EmployeeResponse.GetEmployeeResponse>(_projectMapper)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<EmployeeResponse.GetEmployeeResponse?> GetOneAsync(int id)
    {
        return await _context.TblEmployees
            .Where(e => e.EId == id && e.EStatus != "Z")
            .ProjectTo<EmployeeResponse.GetEmployeeResponse>(_projectMapper)
            .AsNoTracking()
            .FirstOrDefaultAsync();
    }

    public async Task<List<EmployeeResponse.GetEmployeeResponse?>> GetAllEmployeesByDepartmentAsync(int departmentId)
    {
        return (await _context.TblEmployees
            .Where(e => e.EStatus != "Z" && e.DepartmentId == departmentId)
            .ProjectTo<EmployeeResponse.GetEmployeeResponse>(_projectMapper)
            .AsNoTracking()
            .ToListAsync())!;
    }

    public async Task<EmployeeResponse.DeactivateEmployeeResponse?> SetEmployeeInactiveAsync(int id)
    {
        var employee = await _context.TblEmployees.FindAsync(id);
        if (employee == null) return null;
        
        var isAlreadyInactive = employee.EStatus == "Z";
        if (!isAlreadyInactive)
        {
            employee.EStatus = "Z";
            await _context.SaveChangesAsync();
        }

        var response = new EmployeeResponse.DeactivateEmployeeResponse()
        {
            EId = id,
            EStatus = employee.EStatus
        };
        response.AlreadyInactive = isAlreadyInactive;

        return response;
    }

    public async Task<EmployeeResponse.ActivateEmployeeResponse?> SetEmployeeActiveAsync(int id)
    {
        var employee = await _context.TblEmployees.FindAsync(id);
        if(employee == null) return null;
        
        var isAlreadyActive = employee.EStatus == "A";
        if(!isAlreadyActive)
        {
            employee.EStatus = "A";
            await _context.SaveChangesAsync();
        }
        
        var response = new EmployeeResponse.ActivateEmployeeResponse()
        {
            EId = id,
            EStatus = employee.EStatus
        };
        response.AlreadyActive = isAlreadyActive;
        
        return response;
    }

    public async Task<EmployeeResponse.CreateEmployeeResponse?> CreateEmployeeAsync(EmployeeRequest.CreateEmployeeRequest employee)
    {
        try
        {
            var employeeExists = await _context.TblEmployees
                .AnyAsync(e => e.ECpf == employee.ECpf);

            if (employeeExists)
            {
                return new EmployeeResponse.CreateEmployeeResponse
                {
                    EmployeeAlreadyExists = true
                };
            }

            employee.EStatus = "A";
            employee.CreatedAt = DateTime.Now;
            
            var employeeEntity = _mapper.Map<TblEmployees>(employee);

            await _context.TblEmployees.AddAsync(employeeEntity);
            await _context.SaveChangesAsync();

            return _mapper.Map<EmployeeResponse.CreateEmployeeResponse>(employeeEntity);
        }
        catch (DbUpdateException ex)
        {
            throw new ApplicationException("Error saving the employee to the database.", ex);
        }
    }

    public async Task<EmployeeResponse.UpdateEmployeeResponse?> UpdateEmployeeAsync(
    int id,
    EmployeeRequest.UpdateEmployeeRequest request)
    {
        try
        {
            var entity = await _context.TblEmployees.FindAsync(id);
            if (entity is null) return null;

            request.UpdatedAt = DateTime.Now;
            
            _mapper.Map(request, entity);

            await _context.SaveChangesAsync();

            return _mapper.Map<EmployeeResponse.UpdateEmployeeResponse>(entity);
        }
        catch (DbUpdateException ex)
        {
            throw new ApplicationException("Error while updating the employee.", ex);
        }
    }
}