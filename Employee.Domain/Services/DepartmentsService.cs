using Employee.Domain.Interfaces;
using Employee.Domain.Interfaces.Repositories;
using Employee.Domain.Interfaces.Services;
using Employee.Domain.Utils;

namespace Employee.Domain.Services;

public class DepartmentsService : ValidationService, IDepartmentsService
{
    private const string CACHE_KEY = "all-departments";
    private readonly IDepartmentsRepository _repository;
    private readonly ICachingService _cache;

    public DepartmentsService(
        IDepartmentsRepository repository,
        ICachingService cache,
        INotificationService notificationService) : base(notificationService)
    {
        _repository = repository;
        _cache = cache;
    }
    
    public async Task<List<TblDepartments>> GetAllDepartments()
    {
        var cached = await _cache.GetAsync<List<TblDepartments>>(CACHE_KEY);
        if(cached != null) return cached;
        
        var departments = await _repository.GetAllDepartmentsAsync();
        await _cache.SetAsync(CACHE_KEY, departments);
        return departments;
    }
}