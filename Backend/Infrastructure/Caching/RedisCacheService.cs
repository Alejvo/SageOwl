using Application.Interfaces;
using StackExchange.Redis;
using System.Text.Json;

namespace Infrastructure.Caching;

public class RedisCacheService : ICacheService
{
    private readonly IDatabase _database;

    public RedisCacheService(IConnectionMultiplexer redis)
    {
        _database = redis.GetDatabase();
    }

    public async Task<T?> GetAsync<T>(string key)
    {
        var value = await _database
            .StringGetAsync(key);

        if (value.IsNullOrEmpty)
            return default;

        return JsonSerializer.Deserialize<T>(value!);
    }

    public async Task RemoveAsync(string key)
         => await _database
                .KeyDeleteAsync(key);

     public async Task SetAsync<T>(string key,T value, TimeSpan expiryTime)
     {
        var json = JsonSerializer.Serialize(value);
        await _database
            .StringSetAsync(key, json,expiryTime);
     }
}
