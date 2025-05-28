namespace Employee.Domain.Interfaces.Repositories;

public interface IEmployeesRepository
{
    Task<List<TblEmployees>> GetAllAsync();
}