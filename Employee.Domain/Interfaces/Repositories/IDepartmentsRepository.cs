namespace Employee.Domain.Interfaces.Repositories;

public interface IDepartmentsRepository
{
    Task<List<TblDepartments>> GetAllDepartmentsAsync();
}