using System;

namespace PaCoSe.Caching
{
    public interface ICacheProvider
    {
        T Get<T>(string cacheKey);

        T GetAndCache<T>(string cacheKey, Func<T> dataLoader, TimeSpan timeout);

        T GetAndCache<T>(string cacheKey, Func<object[], T> dataLoader, TimeSpan timeout, params object[] args);

        void AddOrUpdate(string cacheKey, object data, TimeSpan? timeout = null);

        void AddOrUpdate<T>(string cacheKey, object data);

        void AddOrUpdate<T>(string cacheKey, Func<T> dataLoader, TimeSpan timeout);

        void Remove(string cacheKey);
    }
}
