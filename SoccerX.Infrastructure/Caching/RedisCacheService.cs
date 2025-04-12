using Microsoft.Extensions.Options;
using SoccerX.Common.Configuration;
using StackExchange.Redis;
using Newtonsoft.Json;
using SoccerX.Application.Interfaces.Cache.Redis;

namespace SoccerX.Infrastructure.Caching
{
    public class RedisCacheService : IRedisCacheService
    {
        #region Field
        private readonly IDatabase _database;
        private readonly ISubscriber _subscriber;
        #endregion

        #region Constructor
        public RedisCacheService(ApplicationSettings settings)
        {
            var redisSettings = settings.Redis;
            ConfigurationOptions config;

            if (redisSettings.UseSentinel)
            {
                var sentinelConfig = new ConfigurationOptions
                {
                    CommandMap = CommandMap.Sentinel,
                    TieBreaker = "",
                    AbortOnConnectFail = false
                };

                foreach (var host in redisSettings.SentinelHosts)
                {
                    sentinelConfig.EndPoints.Add(host);
                }

                var sentinelConnection = ConnectionMultiplexer.Connect(sentinelConfig);
                var masterEndpoint = sentinelConnection
                    .GetServer(sentinelConnection.GetEndPoints().First())
                    .SentinelGetMasterAddressByName(redisSettings.MasterName);

                config = new ConfigurationOptions
                {
                    EndPoints = { masterEndpoint! },
                    Password = redisSettings.Password,
                    Ssl = redisSettings.UseSsl,
                    DefaultDatabase = redisSettings.Database,
                    AbortOnConnectFail = false
                };
            }
            else
            {
                config = new ConfigurationOptions
                {
                    EndPoints = { $"{redisSettings.Host}:{redisSettings.Port}" },
                    Password = redisSettings.Password,
                    Ssl = redisSettings.UseSsl,
                    DefaultDatabase = redisSettings.Database,
                    AbortOnConnectFail = false
                };
            }

            var connection = ConnectionMultiplexer.Connect(config);
            _database = connection.GetDatabase();
            _subscriber = connection.GetSubscriber();
        }
        #endregion

        #region Public Method
        public async Task SetAsync<T>(string key, T value, TimeSpan? expiry = null)
        {
            var json = ConvertToJson(value);
            await _database.StringSetAsync(key, json, expiry);
        }

        public async Task<T?> GetAsync<T>(string key)
        {
            var value = await _database.StringGetAsync(key);
            return value.IsNullOrEmpty ? default : JsonConvert.DeserializeObject<T>(value!);
        }

        public async Task<bool> RemoveAsync(string key)
        {
            return await _database.KeyDeleteAsync(key);
        }

        public async Task<bool> ExistsAsync(string key)
        {
            return await _database.KeyExistsAsync(key);
        }

        public async Task SubscribeAsync(string channel, Action<RedisChannel, RedisValue> handler)
        {
            await _subscriber.SubscribeAsync(new RedisChannel(channel, RedisChannel.PatternMode.Auto), handler);
        }

        public async Task PublishAsync(string channel, string message)
        {
            await _subscriber.PublishAsync(new RedisChannel(channel, RedisChannel.PatternMode.Auto), message);
        }

        public Task AddToListAsync<T>(string key, T value)
        {
            //var json = ConvertToJson(value);
            //await _database.ListRightPushAsync(key, json);
            return Task.CompletedTask;
        }

        public Task AddToFrontOfListAsync<T>(string key, T value)
        {
            //var json = ConvertToJson(value);
            //await _database.ListLeftPushAsync(key, json);
            return Task.CompletedTask;
        }

        public async Task PopFromListAsync<T>(string key)
        {
            await _database.ListLeftPopAsync(key);
        }

        #endregion

        #region Private Method

        private string ConvertToJson(object? value)
        {
            return JsonConvert.SerializeObject(value);
        }

        #endregion
    }
}
