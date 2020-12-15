using PaCoSe.Models;

namespace PaCoSe.API.Models
{
    public class DeviceBroadcastRequest
    {
        public DeviceBroadcastRequest()
        {
            this.Device = new Device();
        }

        public string Code { get; set; }

        public Device Device { get; set; }
    }
}
