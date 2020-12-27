namespace PaCoSe.Models
{
    public class CachedDevice
    {
        public int DeviceId { get; set; }

        public string DeviceName { get; set; }

        public string DeviceIdentifierHash { get; set; }

        public string DeviceToken { get; set; }

        public int ChildId { get; set; }

        public string ChildUsername { get; set; }
    }
}
