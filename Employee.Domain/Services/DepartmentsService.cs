using Employee.Domain.Interfaces;
using Employee.Domain.Interfaces.Repositories;
using Employee.Domain.Interfaces.Services;
using Employee.Domain.Models.Responses;
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

    public async Task<DepartmentResponse.ActivateDepartment?> SetDepartmentActive(int departmentId)
    {
        var res = await _repository.SetDepartmentActiveAsync(departmentId);

        if (res == null)
        {
            AddMessage($"No department found with id {departmentId}");
            return null;
        }

        if (res.AlreadyActive)
        {
            AddMessage($"Department with id {departmentId} is already active");
            return null;
        }
        
        await _cache.RemoveAsync(CACHE_KEY); 
        return res;
    }

    public async Task<DepartmentResponse.DeactivateDepartment?> SetDepartmentInactive(int departmentId)
    {
        var res = await _repository.SetDepartmentInactiveAsync(departmentId); 

        if (res == null)
        {
            AddMessage($"No department found with id {departmentId}");
            return null;
        }
        
        if (res.AlreadyInactive) 
        {
            AddMessage($"Department with id {departmentId} is already inactive");
            return null;
        }
    
        await _cache.RemoveAsync(CACHE_KEY);
        return res;
    }
}