using PaCoSe.Models;

namespace PaCoSe.Contracts
{
    public interface IDeviceService
    {
        bool BroadcastAvailableDevice(string code, Device device);

        DeviceConfig GetDeviceConfig(int id);

        Device AddDevice(string code);

        bool RemoveDevice(int id);

        DeviceConfig AddLimits(int id, DeviceConfig deviceConfig);

        bool AddOwnerToDevice(int id, User user);

        bool RemoveOwnerFromDevice(int id, int ownerId);
    }
}
