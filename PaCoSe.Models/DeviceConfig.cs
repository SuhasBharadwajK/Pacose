﻿using System.Collections.Generic;

namespace PaCoSe.Models
{
    public class DeviceConfig
    {
        public DeviceConfig()
        {
            this.DeviceLimits = new List<DeviceLimit>();
        }

        public string Name { get; set; }

        public bool IsScreenTimeEnabled { get; set; }

        public List<DeviceLimit> DeviceLimits { get; set; }
    }
}
