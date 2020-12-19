using Newtonsoft.Json;
using PaCoSe.Models;
using System.Collections.Generic;

namespace PaCoSe.Core.Mappings
{
    public class DeviceMappingProfile : MappingProfile
    {
        public DeviceMappingProfile()
        {
            this.CreateMap<Device, Data.Model.Device>()
                .ForMember(d => d.DeviceLimits, opt => opt.MapFrom(src => JsonConvert.SerializeObject(src.DeviceLimits ?? new List<DeviceLimit> { })));

            this.CreateMap<Data.Model.Device, Device>()
                .ForMember(d => d.DeviceLimits, opt => opt.MapFrom(src => !string.IsNullOrEmpty(src.DeviceLimits)
                                                                            ? JsonConvert.DeserializeObject<List<DeviceLimit>>(src.DeviceLimits)
                                                                            : new List<DeviceLimit>()));
            this.CreateMap<DeviceToken, Data.Model.DeviceToken>();
            this.CreateMap<Data.Model.DeviceToken, DeviceToken>();

            this.CreateMap<DeviceOwner, Data.Model.DeviceOwner>();
            this.CreateMap<Data.Model.DeviceOwner, DeviceOwner>();

            this.CreateMap<Data.Model.DeviceOwnerView, Device>()
                .ForMember(d => d.DeviceLimits, opt => opt.MapFrom(src => !string.IsNullOrEmpty(src.DeviceLimits)
                                                                            ? JsonConvert.DeserializeObject<List<DeviceLimit>>(src.DeviceLimits)
                                                                            : new List<DeviceLimit>()));

            this.CreateMap<Data.Model.DeviceTokenView, Device>()
                .ForMember(d => d.DeviceLimits, opt => opt.MapFrom(src => !string.IsNullOrEmpty(src.DeviceLimits)
                                                                            ? JsonConvert.DeserializeObject<List<DeviceLimit>>(src.DeviceLimits)
                                                                            : new List<DeviceLimit>()));
        }
    }
}
