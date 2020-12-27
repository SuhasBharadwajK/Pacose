using Microsoft.AspNetCore.Mvc;
using PaCoSe.Contracts;
using PaCoSe.Models;

namespace PaCoSe.API.Controllers
{
    [Route("api/authorize-device")]
    [ApiController]
    public class DeviceAuthorizationController : BaseApiController
    {
        private IDeviceContract DeviceContract { get; set; }

        public DeviceAuthorizationController(IDeviceContract deviceContract)
        {
            this.DeviceContract = deviceContract;
        }

        // POST /broadcast -> Anonymous
        [HttpPost("broadcast")]
        public CachedDevice BroadcastAvailableDevice(AuthorizationRequest authorizationRequest)
        {
            return this.DeviceContract.AddDeviceBroadcastRequest(authorizationRequest);
        }
    }
}
