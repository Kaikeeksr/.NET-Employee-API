namespace Employee.Domain.Interfaces.Services;

public interface ICachingService
{
    Task<T> GetAsync<T>(string key);
    Task SetAsync<T>(string key, T value);
    Task RemoveAsync(string key);
}