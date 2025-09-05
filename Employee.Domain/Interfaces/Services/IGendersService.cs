namespace Employee.Domain.Interfaces.Services;

public interface IGendersService
{
    Task<List<TblGenders>> GetAllGenders();
}