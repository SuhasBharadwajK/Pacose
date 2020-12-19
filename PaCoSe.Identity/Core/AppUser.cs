using Microsoft.AspNetCore.Identity;

namespace PaCoSe.Identity.Core
{
    public class AppUser : IdentityUser
    {
        public string DisplayName { get; set; }
    }
}
