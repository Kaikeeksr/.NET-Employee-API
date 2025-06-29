using Employee.Domain.Models.Responses;

namespace Employee.Domain.Interfaces.Services;

public interface IDepartmentsService
{
    Task<List<TblDepartments>> GetAllDepartments();
    Task<DepartmentResponse.ActivateDepartment?> SetDepartmentActive(int departmentId);
    Task<DepartmentResponse.DeactivateDepartment?> SetDepartmentInactive(int departmentId);
}