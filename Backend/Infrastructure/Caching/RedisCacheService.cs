using Application.Interfaces;
using Microsoft.Extensions.Logging;
using StackExchange.Redis;
using System.Text.Json;

namespace Infrastructure.Caching;

public class RedisCacheService : ICacheService
{
    private readonly IDatabase _database;
    private readonly ILogger<RedisCacheService> _logger;

    public RedisCacheService(IConnectionMultiplexer redis, ILogger<RedisCacheService> logger)
    {
        _database = redis.GetDatabase();
        _logger = logger;
    }

    public async Task<T?> GetAsync<T>(string key)
    {
        try
        {
            var value = await _database
                .StringGetAsync(key);

            if (value.IsNullOrEmpty)
                return default;

            return JsonSerializer.Deserialize<T>(value!);
        }
        catch (Exception ex)
        {
            _logger.LogWarning(ex, "Redis error retrieving the {Key}:", key);
            return default; 
        }

    }

    public async Task RemoveAsync(string key)
    {
        try
        {
            await _database
                .KeyDeleteAsync(key);
        }
        catch(Exception ex)
        {
            _logger.LogWarning(ex, "Redis error deleting the {Key}:", key);
        }
    }

     public async Task SetAsync<T>(string key,T value, TimeSpan expiryTime)
     {
        try
        {
            var json = JsonSerializer.Serialize(value);
            await _database
                .StringSetAsync(key, json, expiryTime);
        }
        catch (Exception ex) 
        {
            _logger.LogWarning(ex, "Redis error while setting the {Key}:", key);
        }

    }
}
