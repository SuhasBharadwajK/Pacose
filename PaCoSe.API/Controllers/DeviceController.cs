using Microsoft.AspNetCore.Mvc;
using PaCoSe.API.Controllers.Core;
using PaCoSe.Contracts;
using PaCoSe.Infra.Context;
using PaCoSe.Models;

namespace PaCoSe.API.Controllers
{
    [Route("api/[controller]")]
    public class DeviceController : BaseAuthorizedApiController
    {
        private IDeviceContract DeviceContract { get; set; }

        private IUsersContract UsersContract { get; set; }

        public DeviceController(IUsersContract usersContract, IDeviceContract deviceContract, IRequestContext requestContext) : base(requestContext)
        {
            this.DeviceContract = deviceContract;
            this.UsersContract = usersContract;
        }

        // POST /validate -> Device Token
        [HttpPut("validate/{id}")]
        public bool ValidateDevice(int id, [FromBody] string code)
        {
            return this.DeviceContract.ValidateDevice(id, code);
        }

        // GET /:id/status -> Device Token
        [HttpGet("{id}/status")]
        public DeviceConfig GetDeviceConfig(int id)
        {
            return this.DeviceContract.GetDeviceConfig(id);
        }

        // POST /add -> User Token
        [HttpPost("own")]
        public Device OwnDevice([FromBody] string code)
        {
            return null;
        }

        // DELETE /:id -> User Token
        [HttpDelete("{id}")]
        public bool RemoveDevice(int id)
        {
            return false;
        }

        // PUT /:id/limits -> User Token
        [HttpPut("{id}/limits")]
        public DeviceConfig AddLimits(int id, DeviceConfig deviceConfig)
        {
            return null;
        }

        // PUT /add-owner/:id -> User Token
        [HttpPut("{id}/add-owner")]
        public bool AddOwnerToDevice(int id, User user)
        {
            return false;
        }

        [HttpPut("{id}/remove-owner/{ownerId}")]
        public bool RemoveOwnerFromDevice(int id, int ownerId)
        {
            return false;
        }
    }
}
