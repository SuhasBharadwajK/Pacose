using System.Collections.Generic;

namespace PaCoSe.Models
{
    public class User
    {
        public User()
        {
            this.UserProfile = new UserProfile();
        }

        public int Id { get; set; }

        public string Sub { get; set; }

        public string Username { get; set; }

        public bool IsActivated { get; set; }

        public bool IsInvited { get; set; }

        public UserProfile UserProfile { get; set; }

        public List<Device> OwnedDevices { get; set; }
    }
}
