using Employee.Domain.Interfaces;
using Employee.Domain.Interfaces.Repositories;
using Employee.Domain.Interfaces.Services;
using Employee.Domain.Utils;

namespace Employee.Domain.Services;

public class GendersService : ValidationService, IGendersService
{
    private const string CACHE_KEY = "all-genders";
    private readonly IGendersRepository _repository;
    private readonly ICachingService _cache;
    
    public GendersService(
        IGendersRepository repository,
        ICachingService cache,
        INotificationService notificationService) : base(notificationService)
    {
        _repository = repository;
        _cache = cache;
    }

    public async Task<List<TblGenders>> GetAllGenders()
    {
        var cached = await _cache.GetAsync<List<TblGenders>>(CACHE_KEY);
        if(cached != null) return cached;
        
        var  genders = await _repository.GetAllGendersAsync();
        await _cache.SetAsync(CACHE_KEY, genders);
        return genders;
    }
}