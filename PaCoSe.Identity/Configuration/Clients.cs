using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace PaCoSe.Identity.Configuration
{
    public class Clients
    {
        public static IEnumerable<Client> Get()
        {
            // TODO: Move this to the Identity DB
            return new List<Client>
            {
                new Client
                {
                    RequireConsent = false,
                    ClientId = "32833d91-156a-4667-b7f4-b2512045ad6e",
                    ClientName = "PaCoSe Local",

                    AccessTokenType = AccessTokenType.Jwt,
                    AccessTokenLifetime = 28800,
                    IdentityTokenLifetime = 30,

                    AllowedGrantTypes = GrantTypes.Code,
                    RequireClientSecret = false,
                    RequirePkce = true,

                    AllowAccessTokensViaBrowser = true,
                    RedirectUris =
                    {
                        "http://localhost:4200/silent-renew.html",
                        "http://localhost:4200/callback",
                        "http://localhost:57215/silent-renew.html",
                        "http://localhost:57215/callback",
                        "https://localhost:44399/silent-renew.html",
                        "https://localhost:44399/callback",
                    },

                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "api.read",
                    },
                    PostLogoutRedirectUris =
                    {
                        "http://localhost:4200/",
                        "http://localhost:57215/",
                        "https://localhost:44399/",
                    },
                    AllowedCorsOrigins =
                    {
                        "http://localhost:4200",
                        "http://localhost:57215",
                        "https://localhost:44399",
                    },
                },
            };
        }
    }
}
