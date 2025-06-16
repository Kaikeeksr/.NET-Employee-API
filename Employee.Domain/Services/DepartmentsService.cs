using Employee.Domain.Interfaces;
using Employee.Domain.Interfaces.Repositories;
using Employee.Domain.Interfaces.Services;
using Employee.Domain.Utils;

namespace Employee.Domain.Services;

public class DepartmentsService : ValidationService, IDepartmentsService
{
    private readonly IDepartmentsRepository _repository;

    public DepartmentsService(
        IDepartmentsRepository repository,
        INotificationService notificationService) : base(notificationService)
    {
        _repository = repository;
    }
    
    public Task<List<TblDepartments>> GetAllDepartments()
    {
        return _repository.GetAllDepartmentsAsync();
    }
}