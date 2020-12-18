using PaCoSe.Caching;

namespace PaCoSe.Providers
{
    public class BaseProvider
    {
        protected ICacheProvider CacheProvider { get; set; }

        public BaseProvider(ICacheProvider cacheProvider)
        {
            this.CacheProvider = cacheProvider;
        }
    }
}
