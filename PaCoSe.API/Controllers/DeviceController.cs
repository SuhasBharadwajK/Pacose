using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaCoSe.API.Models;
using PaCoSe.Contracts;
using PaCoSe.Models;

namespace PaCoSe.API.Controllers
{
    [Route("api/[controller]")]
    public class DeviceController : BaseApiController
    {
        public DeviceController(IUsersContract usersContract)
        {
        }

        // POST /broadcast -> Anonymous
        [HttpPost("broadcast")]
        public bool BroadcastAvailableDevice(DeviceBroadcastRequest deviceBroadcastRequest)
        {
            return false;
        }

        // GET /:id/status -> Device Token
        [HttpGet("{id}/status")]
        public DeviceConfig GetDeviceConfig(int id)
        {
            return null;
        }

        // POST /validate -> Device Token

        // POST /add -> User Token
        [HttpPost("add")]
        public Device AddDevice([FromBody] string code)
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
