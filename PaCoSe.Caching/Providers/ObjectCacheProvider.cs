using System;
using System.Runtime.Caching;

namespace PaCoSe.Caching.Providers
{
    public class ObjectCacheProvider : ICacheProvider
    {
        private ObjectCache Cache
        {
            get
            {
                return MemoryCache.Default;
            }
        }

        private TimeSpan DefaultTimeOut { get; set; }

        public ObjectCacheProvider(TimeSpan defaultTimeOut)
        {
            this.DefaultTimeOut = defaultTimeOut;
        }

        public void AddOrUpdate<T>(string cacheKey, object data)
        {
            this.AddOrUpdate(cacheKey, data);
        }

        public void AddOrUpdate<T>(string cacheKey, Func<T> dataLoader, TimeSpan timeout)
        {
            var items = dataLoader();
            this.AddOrUpdate(cacheKey, items, timeout);
        }

        public void AddOrUpdate(string cacheKey, object data, TimeSpan? timeout = null)
        {
            if (data != null && cacheKey != null)
            {
                CacheItemPolicy policy = new CacheItemPolicy();
                policy.AbsoluteExpiration = DateTime.UtcNow + (timeout ?? this.DefaultTimeOut);

                if (this.Cache.Contains(cacheKey))
                {
                    this.Cache.Set(new CacheItem(cacheKey, data), policy);
                }
                else
                {
                    this.Cache.Add(new CacheItem(cacheKey, data), policy);
                }
            }
        }

        public T Get<T>(string cacheKey)
        {
            if (this.Cache.Contains(cacheKey))
            {
                return (T)this.Cache[cacheKey];
            }

            return default(T);
        }

        public T GetAndCache<T>(string cacheKey, Func<T> dataLoader, TimeSpan timeout)
        {
            var items = this.Get<T>(cacheKey);
            if (items == null)
            {
                items = dataLoader();
                this.AddOrUpdate(cacheKey, items, timeout);
            }

            return items;
        }

        public T GetAndCache<T>(string cacheKey, Func<object[], T> dataLoader, TimeSpan timeout, params object[] args)
        {
            var data = this.Get<T>(cacheKey);
            if (data == null)
            {
                data = dataLoader(args);
                this.AddOrUpdate(cacheKey, data, timeout);
            }

            return data;
        }

        public void Remove(string cacheKey)
        {   
            if (this.Cache.Contains(cacheKey))
            {
                this.Cache.Remove(cacheKey);
            }
        }

        public void Remove(string[] cacheKeys)
        {
            foreach (var cacheKey in cacheKeys)
            {
                if (this.Cache.Contains(cacheKey))
                {
                    this.Cache.Remove(cacheKey);
                }
            }
        }
    }
}
