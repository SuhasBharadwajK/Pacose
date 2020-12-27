using PaCoSe.Models;

namespace PaCoSe.Contracts
{
    public interface IDeviceContract
    {
        void UpdateDeviceName(int id, string name);

        CachedDevice AddDeviceBroadcastRequest(AuthorizationRequest authorizationRequest);

        DeviceConfig GetDeviceConfig(string deviceIdentifierHash, int childId);

        Device AddNewDeviceClaim(string code, User user);

        Device GetDevice(int deviceId);

        bool ValidateDevice(int deviceId, int childId, string code);

        bool RemoveDevice(int deviceId);

        string RefreshDeviceToken(int deviceId);

        DeviceConfig AddLimits(int deviceId, int childId, DeviceConfig deviceConfig);

        bool ToggleScreenTime(int deviceId, int childId);

        bool AddOwnerToDevice(int deviceId, User user);

        bool RemoveOwnerFromDevice(int deviceId, int ownerId);
    }
}
