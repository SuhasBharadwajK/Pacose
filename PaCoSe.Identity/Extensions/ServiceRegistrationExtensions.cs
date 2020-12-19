using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using PaCoSe.Caching;
using PaCoSe.Caching.Factories;
using System;
using System.Linq;
using System.Reflection;

namespace PaCoSe.Identity.Extensions
{
    public static class ServiceRegistrationExtensions
    {
        public static void RegisterDatabaseProviders(this IServiceCollection services, string connectionString, string dbProvider)
        {
            services.AddTransient<PetaPoco.IDatabase>((serviceProvider) =>
            {
                return new PetaPoco.Database(connectionString, dbProvider);
            });
        }

        public static void RegisterCacheProviders(this IServiceCollection services, bool isRedisEnabled, string redisConnectionStrings, int defaultTimeoutInMinutes)
        {
            services.AddSingleton<ICacheFactory>((serviceProvider) => new CacheFactory(isRedisEnabled, redisConnectionStrings, TimeSpan.FromMinutes(defaultTimeoutInMinutes)));
            services.AddSingleton<ICacheProvider>((serviceProvider) => serviceProvider.GetService<ICacheFactory>().GetCacheProvider());
        }

        public static void RegisterMappingProfiles(this IServiceCollection services)
        {
            var appRootAssemblyName = Assembly.GetExecutingAssembly().FullName.Split(", ").First().Split('.').First();
            var allAssemblies = AppDomain.CurrentDomain.GetAssemblies().Where(assembly => assembly.FullName.StartsWith(appRootAssemblyName));
            services.AddAutoMapper(allAssemblies);
        }
    }
}
