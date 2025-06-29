using System.Text.Json;
using Employee.Domain.Interfaces.Services;
using Microsoft.Extensions.Caching.Distributed;

namespace Employee.Domain.Utils;

public class CachingService : ICachingService
{
    private readonly IDistributedCache _cache;
    private readonly DistributedCacheEntryOptions _options;

    public CachingService(IDistributedCache cache)
    {
        _cache = cache;
        _options = new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(1),
            SlidingExpiration = TimeSpan.FromMinutes(30)
        };
    }

    public async Task<T> GetAsync<T>(string key)
    {
        var json = await _cache.GetStringAsync(key);
        if (!string.IsNullOrEmpty(json)) return JsonSerializer.Deserialize<T>(json)!;
        return default;
    }

    public async Task SetAsync<T>(string key, T value)
    {
        var json = JsonSerializer.Serialize(value);
        await _cache.SetStringAsync(key, json, _options);
    }

    public Task RemoveAsync(string key)
    {
        _cache.Remove(key);
        return Task.CompletedTask;
    }
}