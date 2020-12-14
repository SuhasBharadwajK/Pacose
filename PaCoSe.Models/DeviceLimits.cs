using System.Collections.Generic;

namespace PaCoSe.Models
{
    public class DeviceLimit
    {
        public DeviceLimit()
        {
            this.Intervals = new List<LimitInterval>();
        }

        public DayOfTheWeek DayOfTheWeek { get; set; }

        public decimal AllowedHours { get; set; }

        public List<LimitInterval> Intervals { get; set; }
    }
}
