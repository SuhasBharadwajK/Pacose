using AutoMapper;
using Newtonsoft.Json;
using PaCoSe.Caching;
using PaCoSe.Contracts;
using PaCoSe.Core.Extensions;
using PaCoSe.Exceptions;
using PaCoSe.Infra.Persistence;
using PaCoSe.Models;
using System;
using System.Text;

namespace PaCoSe.Providers
{
    public class DeviceProvider : BaseProvider, IDeviceContract
    {
        private const string DeviceLabel = "DEVICE";

        private const string TransientDeviceLabel = "TRANSIENT_DEVICE";

        private IUsersContract UsersContract { get; set; }

        private DateTime TokenValidTill
        {
            get
            {
                return DateTime.UtcNow.AddDays(30);
            }
        }

        public DeviceProvider(ICacheProvider cacheProvider, IMapper mapper, IAppDatabase appDatabase, IUsersContract usersContract) : base(cacheProvider, mapper, appDatabase)
        {
            this.UsersContract = usersContract;
        }

        public void UpdateDeviceName(int id, string name)
        {
            var existingDevice = this.GetDevice(id);

            // Clear device config cache
            var cacheKey = $"{DeviceLabel}:{existingDevice.IdentifierHash}";
            this.CacheProvider.Remove(cacheKey);

            // Update the device.
            existingDevice.Name = name;
            var deviceDataModel = this.Mapper.MapTo<Data.Model.Device>(existingDevice);
            this.Database.Update(deviceDataModel, new string[] { "Name" });
        }

        public Device OwnDevice(string code, User user)
        {
            var cacheKey = $"{TransientDeviceLabel}:{code}";
            var existingDevice = this.CacheProvider.Get<Device>(cacheKey);

            if (existingDevice == null)
            {
                throw new Exception("A device with this code is not found"); // TODO: Custom exception.
            }

            var device = new Device
            {
                IdentifierHash = existingDevice.IdentifierHash,
                Name = existingDevice.Name,
            };

            try
            {
                this.Database.BeginTransaction();
                var deviceDataModel = this.Mapper.MapTo<Data.Model.Device>(device);
                device.Id = this.Database.Insert(deviceDataModel);

                var validTill = this.TokenValidTill;

                // Create a device token
                var generatedToken = this.GenerateDeviceToken(device.Id, validTill);

                // Save the token
                var deviceToken = new DeviceToken
                {
                    TokenString = generatedToken.TokenString,
                    DeviceId = device.Id,
                    ValidTill = validTill
                };

                var deviceTokenDataModel = this.Mapper.MapTo<Data.Model.DeviceToken>(deviceToken);
                deviceToken.Id = this.Database.Insert(deviceTokenDataModel);

                this.AddOwnerToDevice(device.Id, user);

                this.Database.CompleteTransaction();

                // Update the encoded token in the cache which is served to the client
                if (deviceToken.Id > 0)
                {
                    existingDevice.Token = generatedToken.EncodedTokenString;
                    this.CacheProvider.AddOrUpdate(cacheKey, existingDevice, TimeSpan.FromMinutes(15));
                }

                return device;
            }
            catch (Exception e)
            {
                this.Database.AbortTransaction();
                throw e;
            }
        }

        public DeviceConfig AddLimits(int id, DeviceConfig deviceConfig)
        {
            var device = this.GetDevice(id);
            device.DeviceLimits = deviceConfig.DeviceLimits;
            device.IsScreenTimeEnabled = deviceConfig.IsScreenTimeEnabled;

            var deviceDataModel = this.Mapper.MapTo<Data.Model.Device>(device);
            this.Database.Update(deviceDataModel, new string[] { "IsScreenTimeEnabled", "DeviceLimits" });

            var deviceToken = this.GetDeviceToken(id);
            if (deviceToken == null)
            {
                throw new NotFoundException("An error has occurred while adding device limits");
            }

            var deviceContextCacheKey = $"{CacheKeys.DeviceContext}:{deviceToken.TokenString}";
            var deviceConfigCacheKey = $"{DeviceLabel}:{device.IdentifierHash}";

            // Clear all the device caches so that the latest config will be fetched and cached on the next request.
            this.CacheProvider.Remove(new string[] { deviceConfigCacheKey, deviceContextCacheKey });

            return deviceConfig;
        }

        public Device GetDevice(int id)
        {
            var device = this.Database.FirstOrDefault<Data.Model.Device>("WHERE Id = @0", id);
            return this.Mapper.MapTo<Device>(device);
        }

        public bool AddOwnerToDevice(int id, User user)
        {
            try
            {
                var existingUser = this.UsersContract.GetUserByUsername(user.Username);
                if (existingUser == null)
                {
                    user = this.UsersContract.AddUserWithoutProfile(user);
                }
                else
                {
                    user = existingUser;
                }

                if (!user.IsActivated)
                {
                    // TODO: Send invitation email
                }
                else
                {
                    // TODO: Send notification email
                }

                var deviceOwner = new DeviceOwner
                {
                    DeviceId = id,
                    OwnerId = user.Id
                };

                var deviceOwnerDataModel = this.Mapper.MapTo<Data.Model.DeviceOwner>(deviceOwner);

                deviceOwner.Id = this.Database.Insert(deviceOwnerDataModel);

                return deviceOwner.Id > 0;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Device AddDeviceBroadcastRequest(AuthorizationRequest authorizationRequest)
        {
            var cacheKey = $"{TransientDeviceLabel}:{authorizationRequest.Code}";
            var existingDevice = this.CacheProvider.Get<Device>(cacheKey);
            if (existingDevice == null)
            {
                var device = new Device
                {
                    IdentifierHash = authorizationRequest.DeviceIdentifier,
                    Name = authorizationRequest.DeviceName
                };

                this.CacheProvider.AddOrUpdate(cacheKey, device, TimeSpan.FromMinutes(15));
            }

            return existingDevice;
        }

        public bool ValidateDevice(int id, string code)
        {
            var cacheKey = $"{TransientDeviceLabel}:{code}";
            var existingDevice = this.CacheProvider.Get<Device>(cacheKey);
            var isDeviceValid = existingDevice?.Id == id;
            if (isDeviceValid)
            {
                this.CacheProvider.Remove(cacheKey);
            }

            return isDeviceValid;
        }

        public Device GetDeviceFromIdentifier(string identifierHash)
        {
            var device = this.Database.FirstOrDefault<Data.Model.Device>("WHERE IdentifierHash = @0", identifierHash);
            return this.Mapper.MapTo<Device>(device);
        }

        public DeviceConfig GetDeviceConfig(string identifierHash)
        {
            var cacheKey = $"{DeviceLabel}:{identifierHash}";
            var deviceConfig = this.CacheProvider.Get<DeviceConfig>(cacheKey);
            if (deviceConfig == null)
            {
                var existingDevice = this.GetDeviceFromIdentifier(identifierHash);
                deviceConfig = new DeviceConfig
                {
                    DeviceLimits = existingDevice.DeviceLimits,
                    IsScreenTimeEnabled = existingDevice.IsScreenTimeEnabled,
                    Name = existingDevice.Name,
                };
            }

            return deviceConfig;
        }

        public string RefreshDeviceToken(int deviceId)
        {
            var validTill = this.TokenValidTill;
            // Generate a new token for the device.
            var generatedToken = this.GenerateDeviceToken(deviceId, validTill);

            // Get the current device token that is active and delete it.
            var existingDeviceToken = this.GetDeviceToken(deviceId);

            if (existingDeviceToken == null)
            {
                throw new NotFoundException("No device token present to refresh.");
            }

            // Clear the existing context cache.
            var deviceContextCacheKey = $"{CacheKeys.DeviceContext}:{existingDeviceToken}";
            this.CacheProvider.Remove(deviceContextCacheKey);

            var isDeleted = this.Database.Delete<Data.Model.DeviceToken>(existingDeviceToken.Id) > 0;
            if (isDeleted)
            {
                try
                {
                    // Insert the new token.
                    var newToken = new DeviceToken
                    {
                        ValidTill = validTill,
                        TokenString = generatedToken.TokenString,
                        DeviceId = deviceId
                    };

                    var tokenDataModel = this.Mapper.MapTo<Data.Model.DeviceToken>(newToken);
                    this.Database.Insert(tokenDataModel);

                    // Return the new encoded token.
                    return generatedToken.EncodedTokenString;
                }
                catch (Exception e)
                {
                    throw new Exception("An error occurred while trying to refresh the device token", e); // TODO: Custom exception
                }
            }

            throw new Exception("An error occurred while trying to refresh the device token"); // TODO: Custom exception
        }

        public bool RemoveDevice(int id)
        {
            throw new NotImplementedException();
        }

        public bool RemoveOwnerFromDevice(int deviceId, int ownerId)
        {
            // Get the current device owner.
            var deviceOwner = this.Database.FirstOrDefault<Data.Model.DeviceOwner>("WHERE DeviceId = @0 AND OwnerId = @1 AND IsDeleted = @2", deviceId, ownerId, false);

            if (deviceOwner == null)
            {
                throw new NotFoundException("The selected user does not have ownership of this device");
            }

            // Clear the owners's user cache
            var ownerUser = this.UsersContract.GetUser(ownerId);
            var cacheKey = $"{CacheKeys.UserContext}:{ownerUser.Username}";
            this.CacheProvider.Remove(cacheKey);

            // Delete owner
            var isDeleted = this.Database.Delete<Data.Model.DeviceOwner>(deviceOwner.Id) > 0;
            return isDeleted;
        }

        private DeviceToken GetDeviceToken(int id)
        {
            var deviceTokenDataModel = this.Database.FirstOrDefault<Data.Model.DeviceToken>("WHERE DeviceId = @0 AND IsDeleted = @1", id, false);
            return this.Mapper.MapTo<DeviceToken>(deviceTokenDataModel);
        }

        private GeneratedToken GenerateDeviceToken(int deviceId, DateTime validTill)
        {
            var token = Guid.NewGuid().ToString("N") + Guid.NewGuid().ToString("N");
            var tokenData = new EncodedToken
            { 
                TokenString = token,
                DeviceId = deviceId, 
                ValidTill = validTill.ToString("yyyy-MM-ddTHH:mm:ssZ")
            };

            var serializedTokenData = JsonConvert.SerializeObject(tokenData);
            var encodedToken = Convert.ToBase64String(Encoding.UTF8.GetBytes(serializedTokenData));
            return new GeneratedToken
            {
                EncodedTokenString = encodedToken,
                TokenString = token,
            };
        }
    }
}
