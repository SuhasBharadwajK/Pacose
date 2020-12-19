using IdentityServer4.Models;
using System.Collections.Generic;

namespace PaCoSe.Identity.Configuration
{
    public class Resources
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            // TODO: Move this to the Identity DB
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email(),
                new IdentityResource
                {
                    Name = "role",
                    UserClaims = new List<string> {"role"}
                }
            };
        }

        public static IEnumerable<ApiResource> GetApiResources()
        {
            // TODO: Move this to the Identity DB
            return new List<ApiResource>
            {
                new ApiResource {
                    Name = "api",
                    DisplayName = "API",
                    Description = "API Access",
                    UserClaims = new List<string> {"role"},
                    ApiSecrets = new List<Secret> {new Secret("scopeSecret".Sha256())},
                    Scopes = new List<string>
                    {
                        "api.write",
                        "api.read",
                    },
                }
            };
        }
    }
}
