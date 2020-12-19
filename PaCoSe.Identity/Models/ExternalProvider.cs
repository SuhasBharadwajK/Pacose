namespace PaCoSe.Identity.Models
{
    public class ExternalProvider
    {
        public string DisplayName { get; set; }

        public string AuthenticationScheme { get; set; }

        public string IconFilename { get; set; }

        public string Message { get; set; }
    }
}
