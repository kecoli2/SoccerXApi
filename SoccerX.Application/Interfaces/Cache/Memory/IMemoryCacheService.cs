using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SoccerX.Application.Interfaces.Cache.Memory
{
    public interface IMemoryCacheService
    {
        /// <summary>
        /// Belirtilen anahtara ait değeri getirir.
        /// </summary>
        T? Get<T>(string key);

        /// <summary>
        /// Belirtilen anahtar için cache'e değer ekler. Belirli süre sonra ömrü sona erer.
        /// </summary>
        void Set<T>(string key, T value, TimeSpan expiration);

        /// <summary>
        /// Belirtilen anahtarı cache'den kaldırır.
        /// </summary>
        bool Remove(string key);
    }
}
