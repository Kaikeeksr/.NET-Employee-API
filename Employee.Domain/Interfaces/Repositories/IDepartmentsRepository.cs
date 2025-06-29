using Employee.Domain.Models.Responses;

namespace Employee.Domain.Interfaces.Repositories;

public interface IDepartmentsRepository
{
    Task<List<TblDepartments>> GetAllDepartmentsAsync();
    Task<DepartmentResponse.ActivateDepartment?> SetDepartmentActiveAsync(int departmentId);
    Task<DepartmentResponse.DeactivateDepartment?> SetDepartmentInactiveAsync(int departmentId);
}