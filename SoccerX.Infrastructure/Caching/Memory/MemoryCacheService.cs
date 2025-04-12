using SoccerX.Application.Interfaces.Cache.Memory;
using System.Collections.Concurrent;

namespace SoccerX.Infrastructure.Caching.Memory
{
    public class MemoryCacheService: IMemoryCacheService, IDisposable
    {
        #region Field
        private readonly ConcurrentDictionary<string, CacheItem> _cache = new ConcurrentDictionary<string, CacheItem>();
        private readonly Timer _cleanupTimer;

        #endregion

        #region Constructor
        public MemoryCacheService() : this(TimeSpan.FromSeconds(60))
        {
        }

        public MemoryCacheService(TimeSpan cleanupInterval)
        {
            // Timer, _cleanupInterval süresi sonunda başlayıp, her _cleanupInterval'de temizleme yapar.
            _cleanupTimer = new Timer(Cleanup!, null, cleanupInterval, cleanupInterval);
        }
        #endregion

        #region Public Method
        /// <summary>
        /// Cache'den bir öğeyi getirir. Eğer öğe süresi dolmuşsa geri döndürmez.
        /// </summary>
        public T? Get<T>(string key)
        {
            if (!_cache.TryGetValue(key, out var cacheItem)) return default(T);
            if (cacheItem.Expiration > DateTimeOffset.Now)
            {
                return (T)cacheItem.Value!;
            }
            else
            {
                // Süresi dolmuş öğe bulunduğunda hemen kaldır.
                Remove(key);
            }
            return default(T);
        }

        /// <summary>
        /// Cache'e yeni öğe ekler veya var olan öğeyi günceller.
        /// </summary>
        public void Set<T>(string key, T value, TimeSpan expiration)
        {
            var cacheItem = new CacheItem
            {
                Value = value,
                Expiration = DateTimeOffset.Now.Add(expiration)
            };

            _cache[key] = cacheItem;
        }

        /// <summary>
        /// Belirtilen anahtarı cache'den kaldırır.
        /// </summary>
        public bool Remove(string key)
        {
            return _cache.TryRemove(key, out _);
        }

        /// <summary>
        /// Timer tarafından çağrılarak süresi dolmuş öğeleri temizler.
        /// </summary>
        private void Cleanup(object state)
        {
            var now = DateTimeOffset.Now;
            // Cache üzerindeki tüm öğeleri dolaş ve süresi dolmuş olanları tespit et.
            var keysToRemove = (from kvp in _cache where kvp.Value.Expiration <= now select kvp.Key).ToList();
            // Tespit edilen anahtarları temizle.
            foreach (var key in keysToRemove)
            {
                Remove(key);
            }
        }

        /// <summary>
        /// Kullanım tamamlandığında Timer'ı dispose eder.
        /// </summary>
        public void Dispose()
        {
            _cleanupTimer.Dispose();
        }
        #endregion

        #region Private Method
        #endregion
    }

    internal class CacheItem
    {
        public object? Value { get; set; }
        public DateTimeOffset Expiration { get; set; }
    }
}
