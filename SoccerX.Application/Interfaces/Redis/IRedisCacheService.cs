using System;
using System.Threading.Tasks;

namespace SoccerX.Application.Interfaces.Redis
{
    public interface IRedisCacheService
    {
        Task SetAsync<T>(string key, T value, TimeSpan? expiry = null);
        Task<T?> GetAsync<T>(string key);
        Task<bool> RemoveAsync(string key);
        Task<bool> ExistsAsync(string key);
        //Task SubscribeAsync(string channel, Action<RedisChannel, RedisValue> handler);
        Task PublishAsync(string channel, string message);

        /// <summary>
        /// List'in sonuna eleman ekler (RPUSH)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        Task AddToListAsync<T>(string key, T value);

        /// <summary>
        /// List'in başına eleman ekler (LPUSH)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        Task AddToFrontOfListAsync<T>(string key, T value);

        /// <summary>
        /// List'ten eleman çeker (LPOP - baştan çıkarır)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        Task PopFromListAsync<T>(string key);
    }

}
