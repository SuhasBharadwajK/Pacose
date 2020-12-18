using PaCoSe.Caching;
using PaCoSe.Contracts;
using PaCoSe.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaCoSe.Providers
{
    public class DeviceProvider : BaseProvider, IDeviceContract
    {
        public DeviceProvider(ICacheProvider cacheProvider) : base(cacheProvider)
        {
        }

        public Device OwnDevice(string code)
        {
            var existingDevice = this.CacheProvider.Get<TransientDevice>(code);
            var device = new Device
            {
                IdentifierHash = existingDevice.IdentifierHash,
                Name = existingDevice.Name,
            };

            // TODO: Add device to the DB, get the ID, assign it to device object

            return device;
        }

        public DeviceConfig AddLimits(int id, DeviceConfig deviceConfig)
        {
            throw new NotImplementedException();
        }

        public bool AddOwnerToDevice(int id, User user)
        {
            throw new NotImplementedException();
        }

        public TransientDevice AddDeviceBroadcastRequest(AuthorizationRequest authorizationRequest)
        {
            var existingDevice = this.CacheProvider.Get<TransientDevice>(authorizationRequest.Code);
            if (existingDevice == null)
            {
                var device = new TransientDevice
                {
                    IdentifierHash = authorizationRequest.DeviceIdentifier,
                    Name = authorizationRequest.DeviceName
                };

                this.CacheProvider.AddOrUpdate(authorizationRequest.Code, device, TimeSpan.FromMinutes(15));
            }

            return existingDevice;
        }

        public bool ValidateDevice(int id, string code)
        {
            var existingDevice = this.CacheProvider.Get<TransientDevice>(code);
            var isDeviceValid = existingDevice.Id == id;

            if (isDeviceValid)
            {
                this.CacheProvider.Remove(code);
            }

            return isDeviceValid;
        }

        public DeviceConfig GetDeviceConfig(int id)
        {
            var cacheKey = $"Device:{id}";
            var deviceConfig = this.CacheProvider.Get<DeviceConfig>(cacheKey);
            if (deviceConfig == null)
            {
                var existingDevice = new Device(); // TODO: Get the device from the DB.
                deviceConfig = new DeviceConfig
                {
                    DeviceLimits = existingDevice.DeviceLimits,
                    IsScreenTimeEnabled = existingDevice.IsScreenTimeEnabled
                };
            }

            return deviceConfig;
        }

        public bool RemoveDevice(int id)
        {
            throw new NotImplementedException();
        }

        public bool RemoveOwnerFromDevice(int id, int ownerId)
        {
            throw new NotImplementedException();
        }
    }
}
