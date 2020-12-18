using Newtonsoft.Json;
using StackExchange.Redis;
using System;

namespace PaCoSe.Caching.Providers
{
    public class RedisCacheProvider : ICacheProvider
    {
        private TimeSpan DefaultTimeOut { get; set; }

        public RedisCacheProvider(IDatabase cache, TimeSpan defaultTimeOut)
        {
            this.Cache = cache;
            this.DefaultTimeOut = defaultTimeOut;
        }

        private IDatabase Cache { get; set; }

        public T Get<T>(string cacheKey)
        {
            var redisValue = this.Cache.StringGet(cacheKey);
            return redisValue.HasValue ? JsonConvert.DeserializeObject<T>(redisValue, new JsonSerializerSettings() { DateParseHandling = DateParseHandling.None }) : default(T);
        }

        public T GetAndCache<T>(string cacheKey, Func<T> dataLoader, TimeSpan timeout)
        {
            var data = this.Get<T>(cacheKey);
            if (data == null)
            {
                data = dataLoader();
                this.AddOrUpdate(cacheKey, data, timeout);
            }

            return data;
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

        public void AddOrUpdate(string cacheKey, object data, TimeSpan? timeout = null)
        {
            if (data != null && cacheKey != null)
            {
                var value = JsonConvert.SerializeObject(data);
                this.Cache.StringSet(cacheKey, value, timeout != null && timeout >= TimeSpan.Zero ? timeout : this.DefaultTimeOut);
            }
        }

        public void AddOrUpdate<T>(string cacheKey, object data)
        {
            if (data != null && cacheKey != null)
            {
                var value = JsonConvert.SerializeObject(data);
                this.Cache.StringSet(cacheKey, value);
            }
        }

        public void AddOrUpdate<T>(string cacheKey, Func<T> dataLoader, TimeSpan timeout)
        {
            var items = dataLoader();
            this.AddOrUpdate(cacheKey, items, timeout);
        }

        public void Remove(string cacheKey)
        {
            this.Cache.KeyDelete(cacheKey);
        }

        public void Remove(string[] cacheKeys)
        {
            foreach (var cacheKey in cacheKeys)
            {
                this.Cache.KeyDelete(cacheKey);
            }
        }
    }
}
