using PaCoSe.Contracts;
using PaCoSe.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PaCoSe.Providers
{
    public class DeviceProvider : IDeviceService
    {
        public Device AddDevice(string code)
        {
            throw new NotImplementedException();
        }

        public DeviceConfig AddLimits(int id, DeviceConfig deviceConfig)
        {
            throw new NotImplementedException();
        }

        public bool AddOwnerToDevice(int id, User user)
        {
            throw new NotImplementedException();
        }

        public bool BroadcastAvailableDevice(string code, Device device)
        {
            throw new NotImplementedException();
        }

        public DeviceConfig GetDeviceConfig(int id)
        {
            throw new NotImplementedException();
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
