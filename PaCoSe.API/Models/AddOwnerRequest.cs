using PaCoSe.Models;

namespace PaCoSe.API.Models
{
    public class AddOwnerRequest
    {
        public UserProfile User { get; set; }

        public int DeviceIdentifier { get; set; }
    }
}
