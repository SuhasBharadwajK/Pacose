namespace PaCoSe.Models
{
    public class AuthorizationRequest
    {
        public string DeviceIdentifier { get; set; }

        public string DeviceName { get; set; }

        public string ChildUsername { get; set; }

        public string Code { get; set; }
    }
}
