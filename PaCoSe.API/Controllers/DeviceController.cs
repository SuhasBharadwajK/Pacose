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
    public class DeviceController : BaseAuthorizedApiController
    {
        private IDeviceContract DeviceContract { get; set; }

        public DeviceController(IDeviceContract deviceContract, IRequestContext requestContext) : base(requestContext)
        {
            this.DeviceContract = deviceContract;
        }

        // POST /validate [Device Token]
        [HttpPut("validate/{id}")]
        public bool ValidateDevice(int id, [FromBody] string code)
        {
            return this.DeviceContract.ValidateDevice(id, code);
        }

        // GET /status [Device Token]
        [HttpGet("status")]
        public DeviceConfig GetDeviceConfig()
        {
            return this.DeviceContract.GetDeviceConfig(this.RequestContext.Device.IdentifierHash);
        }

        // POST /refresh-token [Device Token]
        [HttpGet("refresh-token")]
        public string RefreshToken()
        {
            return this.DeviceContract.RefreshDeviceToken(this.RequestContext.Device.Id);
        }

        // PUT /update-name/:id [User Token]
        [HttpPut("update-name/{id}")]
        public void UpdateDeviceName(int id, [FromBody] string deviceName)
        {
            // TODO: Move this repititive logic to a decorator.
            if (!this.RequestContext.User.OwnedDevices.Any(d => d.Id == id))
            {
                throw new AccessDeniedException("You don't have access to this device");
            }

            this.DeviceContract.UpdateDeviceName(id, deviceName);
        }

        // POST /own [User Token]
        [HttpPost("own")]
        public Device OwnDevice([FromBody] string code)
        {
            return this.DeviceContract.OwnDevice(code, this.RequestContext.User);
        }

        // POST /disown/:id [User Token]
        [HttpPut("disown/{id}")]
        public bool DisownDevice(int id)
        {
            // TODO: Move this repititive logic to a decorator.
            if (!this.RequestContext.User.OwnedDevices.Any(d => d.Id == id))
            {
                throw new AccessDeniedException("You don't have access to this device");
            }

            return this.DeviceContract.RemoveOwnerFromDevice(id, this.RequestContext.User.Id);
        }

        // DELETE /:id [User Token]
        [HttpDelete("{id}")]
        public bool RemoveDevice(int id)
        {
            return false;
        }

        // PUT /:id/limits [User Token]
        [HttpPut("{id}/limits")]
        public DeviceConfig AddLimits(int id, DeviceConfig deviceConfig)
        {
            // TODO: Move this repititive logic to a decorator.
            if (!this.RequestContext.User.OwnedDevices.Any(d => d.Id == id))
            {
                throw new AccessDeniedException("You don't have access to this device");
            }

            return this.DeviceContract.AddLimits(id, deviceConfig);
        }

        // PUT /add-owner/:id [User Token]
        [HttpPut("{id}/add-owner")]
        public bool AddOwnerToDevice(int id, User user)
        {
            // TODO: Move this repititive logic to a decorator.
            if (!this.RequestContext.User.OwnedDevices.Any(d => d.Id == id))
            {
                throw new AccessDeniedException("You don't have access to this device");
            }

            return this.DeviceContract.AddOwnerToDevice(id, user);
        }

        [HttpPut("{id}/remove-owner/{ownerId}")]
        public bool RemoveOwnerFromDevice(int id, int ownerId)
        {
            // TODO: Move this repititive logic to a decorator.
            if (!this.RequestContext.User.OwnedDevices.Any(d => d.Id == id))
            {
                throw new AccessDeniedException("You don't have access to this device");
            }

            return this.DeviceContract.RemoveOwnerFromDevice(id, ownerId);
        }
    }
}
