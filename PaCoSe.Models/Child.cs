using System.Collections.Generic;

namespace PaCoSe.Models
{
    public class Child
    {
        public Child()
        {
            this.AssignedDevices = new List<Device>();
        }

        public int Id { get; set; }

        public string Username { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public List<Device> AssignedDevices { get; set; }

        public string DisplayName
        {
            get
            {
                return $"{this.FirstName ?? string.Empty} {this.MiddleName ?? string.Empty} {this.LastName ?? string.Empty}".Trim();
            }
        }
    }
}
