using PaCoSe.Caching.Providers;
using StackExchange.Redis;
using System;

namespace PaCoSe.Caching.Factories
{
    public class CacheFactory : ICacheFactory
    {
        private static ConnectionMultiplexer _redisConnection { get; set; }

        public bool IsRedisEnabled { get; set; }

        public string CacheConnectionString { get; set; }

        public TimeSpan DefaultTimeout { get; set; }

        public CacheFactory(bool isRedisEnabled, string cacheConnectionString, TimeSpan defaultTimeout)
        {
            this.IsRedisEnabled = isRedisEnabled;
            this.CacheConnectionString = cacheConnectionString;
            this.DefaultTimeout = defaultTimeout;
        }

        public ICacheProvider GetCacheProvider()
        {
            return this.GetCacheProvider(this.IsRedisEnabled, this.CacheConnectionString, this.DefaultTimeout);
        }

        public ICacheProvider GetCacheProvider(bool isRedisEnabled, string cacheConnectionString, TimeSpan defaultTimeout)
        {
            if (isRedisEnabled && !string.IsNullOrEmpty(cacheConnectionString))
            {
                if (CacheFactory._redisConnection != null && CacheFactory._redisConnection.IsConnected)
                {
                    return new RedisCacheProvider(CacheFactory._redisConnection.GetDatabase(), defaultTimeout);
                }

                CacheFactory._redisConnection = ConnectionMultiplexer.Connect(cacheConnectionString);
                if (CacheFactory._redisConnection.IsConnected)
                {
                    // Return the redis cache provider.
                    return new RedisCacheProvider(CacheFactory._redisConnection.GetDatabase(), defaultTimeout);
                }
            }

            // Return the object cache provider.
            return new ObjectCacheProvider(defaultTimeout);
        }
    }
}
