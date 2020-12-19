namespace PaCoSe.Identity.Core
{
    public class AuthProvider
    {
        public string Name { get; set; }

        public string Authority { get; set; }

        public string ClientId { get; set; }

        public string ClientSecret { get; set; }

        public string IconFilename { get; set; }

        public string Message { get; set; }

        public string CookieName { get; set; }

        public string CssClass { get; set; }
    }
}
