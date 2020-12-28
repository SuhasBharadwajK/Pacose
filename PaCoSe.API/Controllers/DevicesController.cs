using Microsoft.AspNetCore.Mvc;
using PaCoSe.API.Controllers.Core;
using PaCoSe.Contracts;
using PaCoSe.Exceptions;
using PaCoSe.Infra.Context;
using PaCoSe.Models;
using System.Linq;

namespace PaCoSe.API.Controllers
{
    [Route("api/[controller]")]
    public class DevicesController : BaseAuthorizedApiController
    {
        private IDeviceContract DeviceContract { get; set; }

        public DevicesController(IDeviceContract deviceContract, IRequestContext requestContext) : base(requestContext)
        {
            this.DeviceContract = deviceContract;
        }

        // POST /validate/:childId [Device Token]
        [HttpPut("validate/{childId}")]
        public bool ValidateDevice(int childId, [FromBody] string code)
        {
            return this.DeviceContract.ValidateDevice(this.RequestContext.Device.Id, childId, code);
        }

        // GET /status/:childId [Device Token]
        [HttpGet("status/{childId}")]
        public DeviceConfig GetDeviceConfig(int childId)
        {
            return this.DeviceContract.GetDeviceConfig(this.RequestContext.Device.IdentifierHash, childId);
        }

        // POST /refresh-token [Device Token]
        [HttpGet("refresh-token")]
        public string RefreshToken()
        {
            return this.DeviceContract.RefreshDeviceToken(this.RequestContext.Device.Id);
        }

        // PUT /update-name/:deviceId [User Token]
        [HttpPut("update-name/{deviceId}")]
        public void UpdateDeviceName(int deviceId, [FromBody] string deviceName)
        {
            // TODO: Move this repititive logic to a decorator.
            if (!this.RequestContext.User.OwnedDevices.Any(d => d.Id == deviceId))
            {
                throw new AccessDeniedException("You don't have access to this device");
            }

            this.DeviceContract.UpdateDeviceName(deviceId, deviceName);
        }

        // POST /claim-device [User Token]
        [HttpPost("claim-device")]
        public Device ClaimDevice([FromBody] string code)
        {
            return this.DeviceContract.AddNewDeviceClaim(code, this.RequestContext.User);
        }

        // POST /disown/:deviceId [User Token]
        [HttpPut("disown/{deviceId}")]
        public bool DisownDevice(int deviceId)
        {
            // TODO: Move this repititive logic to a decorator.
            if (!this.RequestContext.User.OwnedDevices.Any(d => d.Id == deviceId))
            {
                throw new AccessDeniedException("You don't have access to this device");
            }

            return this.DeviceContract.RemoveOwnerFromDevice(deviceId, this.RequestContext.User.Id);
        }

        // DELETE /:deviceId [User Token]
        [HttpDelete("{deviceId}")]
        public bool RemoveDevice(int deviceId)
        {
            return false;
        }

        // PUT /:deviceId/limits/:childId [User Token]
        [HttpPut("{deviceId}/limits/{childId}")]
        public DeviceConfig AddLimits(int deviceId, int childId, DeviceConfig deviceConfig)
        {
            // TODO: Move this repititive logic to a decorator.
            if (!this.RequestContext.User.OwnedDevices.Any(d => d.Id == deviceId))
            {
                throw new AccessDeniedException("You don't have access to this device");
            }

            return this.DeviceContract.AddLimits(deviceId, childId, deviceConfig);
        }

        // PUT /:deviceId/toggle-screen-time/:childId [User Token]
        [HttpPut("{deviceId}/toggle-screen-time/{childId}")]
        public bool ToggleDeviceLimits(int deviceId, int childId)
        {
            return this.DeviceContract.ToggleScreenTime(deviceId, childId);
        }

        // PUT /:deviceId/add-owner [User Token]
        [HttpPut("{deviceId}/add-owner")]
        public bool AddOwnerToDevice(int deviceId, User user)
        {
            // TODO: Move this repititive logic to a decorator.
            if (!this.RequestContext.User.OwnedDevices.Any(d => d.Id == deviceId))
            {
                throw new AccessDeniedException("You don't have access to this device");
            }

            return this.DeviceContract.AddOwnerToDevice(deviceId, user);
        }

        // PUT /:deviceId/remove-owner/:ownerId [User Token]
        [HttpPut("{deviceId}/remove-owner/{ownerId}")]
        public bool RemoveOwnerFromDevice(int deviceId, int ownerId)
        {
            // TODO: Move this repititive logic to a decorator.
            if (!this.RequestContext.User.OwnedDevices.Any(d => d.Id == deviceId))
            {
                throw new AccessDeniedException("You don't have access to this device");
            }

            return this.DeviceContract.RemoveOwnerFromDevice(deviceId, ownerId);
        }
    }
}
