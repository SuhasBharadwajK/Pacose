using PaCoSe.Models;

namespace PaCoSe.Contracts
{
    public interface IDeviceContract
    {
        void UpdateDeviceName(int id, string name);

        Device AddDeviceBroadcastRequest(AuthorizationRequest authorizationRequest);

        Device GetDeviceFromIdentifier(string token);

        DeviceConfig GetDeviceConfig(string token);

        Device OwnDevice(string code, User user);

        Device GetDevice(int id);

        bool ValidateDevice(int id, string code);

        bool RemoveDevice(int id);

        string RefreshDeviceToken(int deviceId);

        DeviceConfig AddLimits(int id, DeviceConfig deviceConfig);

        bool ToggleDeviceLimits(int id);

        bool AddOwnerToDevice(int id, User user);

        bool RemoveOwnerFromDevice(int id, int ownerId);
    }
}
