using System.Collections.Generic;

namespace PaCoSe.Models
{
    public class Device
    {
        public Device()
        {
            this.DeviceLimits = new List<DeviceLimit>();
        }

        public int Id { get; set; }

        public string IdentifierHash { get; set; }

        public string Name { get; set; }

        public bool IsScreenTimeEnabled { get; set; }

        public string Token { get; set; }

        public List<DeviceLimit> DeviceLimits { get; set; }
    }
}
