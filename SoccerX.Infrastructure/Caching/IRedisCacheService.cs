using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoccerX.Infrastructure.Caching
{
    public interface IRedisCacheService
    {
        Task SetAsync<T>(string key, T value, TimeSpan? expiry = null);
        Task<T?> GetAsync<T>(string key);
        Task<bool> RemoveAsync(string key);
        Task<bool> ExistsAsync(string key);
        Task SubscribeAsync(string channel, Action<RedisChannel, RedisValue> handler);
        Task PublishAsync(string channel, string message);
    }

}
