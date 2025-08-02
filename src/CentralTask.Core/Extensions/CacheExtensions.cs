using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace CentralTask.Core.Extensions;
public static class CacheExtensions
{
    public static async Task<T> SetObject<T>(this IDistributedCache cache, string key, T value, TimeSpan? expiresIn = null)
        where T : class
    {
        var serializedValue = JsonConvert.SerializeObject(value, new JsonSerializerSettings()
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        });

        await cache.SetStringAsync(key, serializedValue, new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = expiresIn
        });

        return value;
    }

    public static async Task<T?> GetObject<T>(this IDistributedCache cache, string key)
        where T : class
    {
        var result = await cache.GetStringAsync(key);

        return result == null ? default : JsonConvert.DeserializeObject<T>(result);
    }
}
