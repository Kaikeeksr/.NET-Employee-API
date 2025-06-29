using Employee.Domain;
using Employee.Domain.Interfaces.Repositories;
using Employee.Domain.Models.Responses;
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

    public async Task<DepartmentResponse.ActivateDepartment?> SetDepartmentActiveAsync(int departmentId)
    {
        var department = await _context.TblDepartments
            .AsNoTracking() 
            .FirstOrDefaultAsync(d => d.Id == departmentId);

        if (department == null)
        {
            return null;
        }

        var wasAlreadyActive = department.IsActive;


        if (!department.IsActive)
        {
            department.IsActive = true;
            _context.TblDepartments.Update(department); 
            
            await _context.SaveChangesAsync();
        }
        
        var response = new DepartmentResponse.ActivateDepartment
        {
            AlreadyActive = wasAlreadyActive,
            DepartmentId = departmentId,
            IsActive = department.IsActive
        };

        return response;
    }

    public async Task<DepartmentResponse.DeactivateDepartment?> SetDepartmentInactiveAsync(int departmentId)
    {
        var department = await _context.TblDepartments
            .AsNoTracking()
            .FirstOrDefaultAsync(d => d.Id == departmentId);


        if (department == null)
        {
            return null;
        }

        var wasAlreadyInactive = !department.IsActive;
        
        if (department.IsActive) 
        {
            department.IsActive = false;
            _context.TblDepartments.Update(department);
            
            await _context.SaveChangesAsync();
        }
        
        var response = new DepartmentResponse.DeactivateDepartment
        {
            DepartmentId = departmentId,
            AlreadyInactive = wasAlreadyInactive, 
            IsActive = department.IsActive
        };

        return response;
    }
}