namespace Employee.Domain.Interfaces.Repositories;

public interface IGendersRepository
{
    Task<List<TblGenders>> GetAllGendersAsync();
}