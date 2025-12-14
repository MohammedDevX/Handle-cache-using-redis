
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Products_service.Services.Caching
{
    public class RedisCacheService : IRedisCacheService
    {
        private IDistributedCache cache;
        public RedisCacheService(IDistributedCache cache)
        {
            this.cache = cache;
        }

        public async Task<T?> GetData<T>(string key)
        {
            var data = await cache.GetStringAsync(key);
            if (data == null)
            {
                return default(T);
            }

            return JsonSerializer.Deserialize<T>(data);
        }

        public async Task SetData<T>(string key, T data)
        {
            var options = new DistributedCacheEntryOptions()
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
            };

            cache.SetString(key, JsonSerializer.Serialize(data), options);
        }
    }
}
