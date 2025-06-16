namespace Employee.Domain.Interfaces.Services;

public interface IDepartmentsService
{
    Task<List<TblDepartments>> GetAllDepartments();
}