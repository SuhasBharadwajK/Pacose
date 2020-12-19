using IdentityServer4.Stores;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using PaCoSe.Identity.Models;
using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace PaCoSe.Identity.Extensions
{
    public static class AuthExtensions
    {
        public static async Task<bool> IsPkceClientAsync(this IClientStore store, string client_id)
        {
            if (!string.IsNullOrWhiteSpace(client_id))
            {
                var client = await store.FindEnabledClientByIdAsync(client_id);
                return client?.RequirePkce == true;
            }

            return false;
        }

        public static IActionResult LoadingPage(this Controller controller, string viewName, string redirectUri)
        {
            controller.HttpContext.Response.StatusCode = 200;
            controller.HttpContext.Response.Headers["Location"] = string.Empty;

            return controller.View(viewName, new RedirectViewModel { RedirectUrl = redirectUri });
        }

        public static IIdentityServerBuilder LoadSigningCredentialFrom(this IIdentityServerBuilder builder, string certificateThumbprint)
        {
            X509Certificate2 cert = null;

            try
            {
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    using (X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser))
                    {
                        store.Open(OpenFlags.ReadOnly);
                        X509Certificate2Collection certCollection = store.Certificates.Find(X509FindType.FindByThumbprint, certificateThumbprint, false);
                        if (certCollection.Count > 0)
                        {
                            cert = certCollection[0];
                        }
                    }
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    var certFileDirectory = "/var/ssl";

                    var privateCertFilePath = $"{certFileDirectory}/private/{certificateThumbprint}.p12";
                    var certFilePath = $"{certFileDirectory}/certs/{certificateThumbprint}.der";

                    if (File.Exists(privateCertFilePath))
                    {
                        var certData = File.ReadAllBytes(privateCertFilePath);
                        cert = new X509Certificate2(certData);
                    }
                    else if (File.Exists(certFilePath))
                    {
                        var certData = File.ReadAllBytes(certFilePath);
                        cert = new X509Certificate2(certData);
                    }
                }

                if (cert != null)
                {
                    builder.AddSigningCredential(cert);
                }
                else
                {
                    builder.AddDeveloperSigningCredential();
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            return builder;
        }
    }
}
