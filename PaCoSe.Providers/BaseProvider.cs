using AutoMapper;
using PaCoSe.Caching;
using PaCoSe.Infra.Persistence;

namespace PaCoSe.Providers
{
    public class BaseProvider
    {
        protected ICacheProvider CacheProvider { get; set; }

        protected IMapper Mapper { get; set; }

        protected IAppDatabase Database { get; set; }

        public BaseProvider(ICacheProvider cacheProvider, IMapper mapper, IAppDatabase appDatabase)
        {
            this.CacheProvider = cacheProvider;
            this.Mapper = mapper;
            this.Database = appDatabase;
        }
    }
}
