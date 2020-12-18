using Newtonsoft.Json;

namespace PaCoSe.Models
{
    public class EncodedToken
    {
        [JsonProperty("deviceId")]
        public int DeviceId { get; set; }

        [JsonProperty("token")]
        public string TokenString { get; set; }

        [JsonProperty("validTill")]
        public string ValidTill { get; set; }
    }
}
