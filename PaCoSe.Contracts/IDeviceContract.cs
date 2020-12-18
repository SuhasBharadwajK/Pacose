using PaCoSe.Models;

namespace PaCoSe.Contracts
{
    public interface IDeviceContract
    {
        TransientDevice AddDeviceBroadcastRequest(AuthorizationRequest authorizationRequest);

        DeviceConfig GetDeviceConfig(int id);

        Device OwnDevice(string code);

        bool ValidateDevice(int id, string code);

        bool RemoveDevice(int id);

        DeviceConfig AddLimits(int id, DeviceConfig deviceConfig);

        bool AddOwnerToDevice(int id, User user);

        bool RemoveOwnerFromDevice(int id, int ownerId);
    }
}
