using Microsoft.Extensions.DependencyInjection;
using PaCoSe.Contracts;
using PaCoSe.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PaCoSe.API.Extensions
{
    public static class DataServicesRegistrationExtensions
    {
        public static void RegisterDataProviders(this IServiceCollection services)
        {
            services.AddScoped<IUsersContract, UsersProvider>();
            services.AddScoped<IDeviceContract, DeviceProvider>();
        }
    }
}
