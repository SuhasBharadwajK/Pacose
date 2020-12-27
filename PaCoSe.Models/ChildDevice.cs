using System.Collections.Generic;

namespace PaCoSe.Models
{
    public class ChildDevice
    {
        public ChildDevice()
        {
            this.DeviceLimits = new List<DeviceLimit>();
        }

        public int ChildId { get; set; }

        public int DeviceId { get; set; }

        public string DeviceName { get; set; }

        public string IdentifierHash { get; set; }

        public bool IsScreenTimeEnabled { get; set; }

        public List<DeviceLimit> DeviceLimits { get; set; }
    }
}
