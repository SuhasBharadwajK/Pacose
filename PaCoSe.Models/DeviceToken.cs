using System;

namespace PaCoSe.Models
{
    public class DeviceToken
    {
        public int Id { get; set; }

        public int DeviceId { get; set; }

        public string TokenString { get; set; }

        public DateTime ValidTill { get; set; }

        public bool IsExpired
        {
            get
            {
                return DateTime.UtcNow > this.ValidTill;
            }
        }
    }
}
