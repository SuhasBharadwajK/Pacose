using System;

namespace PaCoSe.Caching.Factories
{
    public interface ICacheFactory
    {
        ICacheProvider GetCacheProvider();

        ICacheProvider GetCacheProvider(bool isRedisEnabled, string cacheConnectionString, TimeSpan defaultTimeout);
    }
}
