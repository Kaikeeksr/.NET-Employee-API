namespace Employee.Domain.Interfaces.Services;

public interface IEmployeesService
{
    Task<List<TblEmployees>> GetAllEmployees();
}
