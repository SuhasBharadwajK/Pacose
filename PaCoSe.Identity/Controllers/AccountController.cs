using IdentityModel;
using IdentityServer4;
using IdentityServer4.Events;
using IdentityServer4.Extensions;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using PaCoSe.Core.Contracts;
using PaCoSe.Identity.Core;
using PaCoSe.Identity.Extensions;
using PaCoSe.Identity.Models;
using PaCoSe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PaCoSe.Identity.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<AppUser> userManager;

        private readonly SignInManager<AppUser> signInManager;

        private readonly IIdentityServerInteractionService interaction;

        private readonly IClientStore clientStore;

        private readonly IAuthenticationSchemeProvider schemeProvider;

        private readonly IEventService events;

        private ICoreDataContract CoreDataService { get; set; }

        private IConfiguration Configuration { get; set; }

        public AccountController(
            IIdentityServerInteractionService interaction,
            IClientStore clientStore,
            IAuthenticationSchemeProvider schemeProvider,
            IEventService events,
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            IConfiguration configuration,
            ICoreDataContract coreDataService
        )
        {
            this.userManager = userManager;
            this.interaction = interaction;
            this.clientStore = clientStore;
            this.schemeProvider = schemeProvider;
            this.events = events;
            this.signInManager = signInManager;
            this.Configuration = configuration;
            this.CoreDataService = coreDataService;
        }

        [TempData]
        public string ErrorMessage { get; set; }

        [HttpGet]
        public async Task<IActionResult> Login(string returnUrl)
        {
            // build a model so we know what to show on the login page
            var vm = await this.BuildLoginViewModelAsync(returnUrl);

            if (vm.IsExternalLoginOnly)
            {
                // we only have one option for logging in and it's an external provider
                return this.RedirectToAction("Challenge", "External", new { provider = vm.ExternalLoginScheme, returnUrl });
            }

            return this.View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginInputModel model, string button)
        {
            // check if we are in the context of an authorization request
            var context = await this.interaction.GetAuthorizationContextAsync(model.ReturnUrl);

            // the user clicked the "cancel" button
            if (button != "login")
            {
                if (context != null)
                {
                    //// if the user cancels, send a result back into IdentityServer as if they 
                    //// denied the consent (even if this client does not require consent).
                    //// this will send back an access denied OIDC error response to the client.
                    //await this.interaction.GrantConsentAsync(context, ConsentResponse.Denied); // CHECK

                    // we can trust model.ReturnUrl since GetAuthorizationContextAsync returned non-null
                    if (await this.clientStore.IsPkceClientAsync(context.Client.ClientId))
                    {
                        // if the client is PKCE then we assume it's native, so this change in how to
                        // return the response is for better UX for the end user.
                        return this.LoadingPage("Redirect", model.ReturnUrl);
                    }

                    return this.Redirect(model.ReturnUrl);
                }
                else
                {
                    // since we don't have a valid context, then we just go back to the home page
                    return this.Redirect("~/");
                }
            }

            if (ModelState.IsValid)
            {
                var user = await this.userManager.FindByNameAsync(model.Username);

                if (user != null)
                {
                    var result = await this.signInManager.PasswordSignInAsync(user, model.Password, true, false);
                    if (result.Succeeded)
                    {
                    }
                }

                // validate username/password against in-memory store
                if (user != null && await this.userManager.CheckPasswordAsync(user, model.Password))
                {
                    await this.events.RaiseAsync(new UserLoginSuccessEvent(user.UserName, user.Id, user.UserName, clientId: context?.Client?.ClientId));

                    // only set explicit expiration here if user chooses "remember me". 
                    // otherwise we rely upon expiration configured in cookie middleware.
                    AuthenticationProperties props = null;
                    if (AccountOptions.AllowRememberLogin && model.RememberLogin)
                    {
                        props = new AuthenticationProperties
                        {
                            IsPersistent = true,
                            ExpiresUtc = DateTimeOffset.UtcNow.Add(AccountOptions.RememberMeLoginDuration)
                        };
                    }

                    var identityServerUser = new IdentityServerUser(user.Id)
                    {
                        DisplayName = user.UserName
                    };

                    await HttpContext.SignInAsync(identityServerUser, props); // DOUBTFUL

                    if (this.interaction.IsValidReturnUrl(model.ReturnUrl)
                    || Url.IsLocalUrl(model.ReturnUrl))
                    {
                        return this.Redirect(model.ReturnUrl);
                    }

                    return this.Redirect("~/");
                }

                await this.events.RaiseAsync(new UserLoginFailureEvent(model.Username, "invalid credentials", clientId: context?.Client?.ClientId));
                ModelState.AddModelError(string.Empty, AccountOptions.InvalidCredentialsErrorMessage);
            }

            // something went wrong, show form with error
            var vm = await this.BuildLoginViewModelAsync(model);
            return this.View(vm);
        }

        public ActionResult ExternalLogin(string provider, string returnUrl = null)
        {
            var redirectUrl = Url.Action(nameof(this.ExternalLoginCallback), "Account", new { returnUrl });
            var properties = this.signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return this.Challenge(properties, provider);
        }

        [HttpGet]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        {
            if (remoteError != null)
            {
                this.ErrorMessage = $"Error from external provider: {remoteError}";
                return this.RedirectToAction(nameof(this.Login));
            }

            var result = await HttpContext.AuthenticateAsync(IdentityConstants.ExternalScheme);
            if (result?.Succeeded != true)
            {
                this.ErrorMessage = "External authentication error";
                return this.RedirectToAction(nameof(this.Login));
            }

            var externalUser = result.Principal;
            var claims = externalUser.Claims.ToList();

            var userIdClaim = claims.FirstOrDefault(x => x.Type == JwtClaimTypes.Subject);
            if (userIdClaim == null)
            {
                userIdClaim = claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
            }

            if (userIdClaim == null)
            {
                throw new Exception("Unknown userid");
            }

            claims.Remove(userIdClaim);
            var provider = result.Properties.Items["LoginProvider"];
            var userId = userIdClaim.Value;
            var userName = claims.FirstOrDefault(x => x.Type == JwtClaimTypes.PreferredUserName)?.Value ?? claims.FirstOrDefault(x => x.Type == ClaimTypes.Upn)?.Value;
            var displayName = claims.FirstOrDefault(x => x.Type == JwtClaimTypes.Name)?.Value;

            User existingUser;

            try
            {
                existingUser = this.CoreDataService.GetUser(userName);
                if (existingUser == null)
                {
                    var userClaims = claims.Select(c => new { c.Type, c.Value });
                    var reason = existingUser == null ? "User does not exist in the system" : "User's login is disabled";

                    //LogItem[] logs = { new LogItem("Claims", userClaims), new LogItem("Reason", reason) };

                    //this.GetLogger(userId, displayName, userName)
                    //    .LogEvent(Logging.Models.EventType.UnauthorizedLoginAttempt, entityId: userName ?? displayName ?? userId, logItems: logs);
                    return this.RedirectToAction(nameof(this.AccessDenied));
                }
            }
            catch (Exception e)
            {
                throw e;
            }

            var user = await this.userManager.FindByLoginAsync(provider, userId);

            if (user == null)
            {
                user = new AppUser
                {
                    UserName = userName,
                    Email = claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value ?? claims.FirstOrDefault(x => x.Type == ClaimTypes.Upn)?.Value,
                    DisplayName = claims.FirstOrDefault(x => x.Type == JwtClaimTypes.Name)?.Value ?? claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value
                };

                var r1 = await this.userManager.CreateAsync(user);
                var r2 = await this.userManager.AddLoginAsync(user, new UserLoginInfo(provider, userId, provider));
            }

            if (string.IsNullOrEmpty(existingUser.Sub))
            {
                user = string.IsNullOrEmpty(user.Id) ? await this.userManager.FindByLoginAsync(provider, userId) : user;
                this.CoreDataService.UpdateUserSub(userName, user.Id);
            }

            var additionalClaims = new List<Claim>();

            var sid = claims.FirstOrDefault(x => x.Type == JwtClaimTypes.SessionId);
            if (sid != null)
            {
                additionalClaims.Add(new Claim(JwtClaimTypes.SessionId, sid.Value));
            }

            AuthenticationProperties props = null;
            var id_token = result.Properties.GetTokenValue("id_token");
            if (id_token != null)
            {
                props = new AuthenticationProperties();
                props.StoreTokens(new[] { new AuthenticationToken { Name = "id_token", Value = id_token } });
            }

            await this.events.RaiseAsync(new UserLoginSuccessEvent(provider, userId, user.Id, user.UserName));

            await this.signInManager.SignInWithClaimsAsync(user, props, additionalClaims);

            //this.GetLogger(existingUser.Id.ToString(), existingUser.UserProfile.DisplayName, existingUser.Username)
            //    .LogEvent(Logging.Models.EventType.Login, entityId: existingUser.Username);

            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            returnUrl = returnUrl ?? result.Properties.Items["returnUrl"];
            if (this.interaction.IsValidReturnUrl(returnUrl) || Url.IsLocalUrl(returnUrl))
            {
                return this.Redirect(returnUrl);
            }

            return this.Redirect("~/");
        }

        public IActionResult ExternalLoginCallback()
        {
            return this.View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout(string logoutId)
        {
            // build a model so the logout page knows what to display
            var vm = await this.BuildLogoutViewModelAsync(logoutId);

            if (vm.ShowLogoutPrompt == false)
            {
                // if the request for logout was properly authenticated from IdentityServer, then
                // we don't need to show the prompt and can just log the user out directly.
                return await this.Logout(vm);
            }

            return this.View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout(LogoutInputModel model)
        {
            // build a model so the logged out page knows what to display
            var vm = await this.BuildLoggedOutViewModelAsync(model.LogoutId);

            if (User?.Identity.IsAuthenticated == true)
            {
                // delete local authentication cookie
                var context = await this.interaction.GetLogoutContextAsync(model.LogoutId);

                await this.signInManager.SignOutAsync();

                var userName = User.Claims.FirstOrDefault(x => x.Type == JwtClaimTypes.PreferredUserName)?.Value ?? User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Upn)?.Value;
                var existingUser = this.CoreDataService.GetUser(userName);

                //this.GetLogger(existingUser.Id.ToString(), existingUser.UserProfile.DisplayName, existingUser.Username)
                //    .LogEvent(Logging.Models.EventType.Logout, entityId: existingUser.Username);

                // raise the logout event
                await this.events.RaiseAsync(new UserLogoutSuccessEvent(User.GetSubjectId(), User.GetDisplayName()));

                return this.Redirect(context.PostLogoutRedirectUri);
            }

            return this.View("LoggedOut", vm);
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return this.View();
        }

        private async Task<LoginViewModel> BuildLoginViewModelAsync(string returnUrl)
        {
            var context = await this.interaction.GetAuthorizationContextAsync(returnUrl);
            if (context?.IdP != null && await this.schemeProvider.GetSchemeAsync(context.IdP) != null)
            {
                var local = context.IdP == IdentityServer4.IdentityServerConstants.LocalIdentityProvider;

                // this is meant to short circuit the UI and only trigger the one external IdP
                var vm = new LoginViewModel
                {
                    EnableLocalLogin = local,
                    ReturnUrl = returnUrl,
                    Username = context?.LoginHint,
                };

                if (!local)
                {
                    vm.ExternalProviders = new[] { new ExternalProvider { AuthenticationScheme = context.IdP } };
                }

                return vm;
            }

            var schemes = await this.schemeProvider.GetAllSchemesAsync();

            var authProviders = this.Configuration.GetSection("AuthProviders").Get<List<AuthProvider>>();

            var providers = schemes
                .Where(x => x.DisplayName != null ||
                            x.Name.Equals(AccountOptions.WindowsAuthenticationSchemeName, StringComparison.OrdinalIgnoreCase))
                .Select(x => new ExternalProvider
                {
                    DisplayName = x.DisplayName ?? x.Name,
                    AuthenticationScheme = x.Name,
                    IconFilename = authProviders.Find(p => p.Name == x.Name)?.IconFilename,
                    Message = authProviders.Find(p => p.Name == x.Name)?.Message,
                }).ToList();

            var allowLocal = true;
            if (context?.Client?.ClientId != null)
            {
                var client = await this.clientStore.FindEnabledClientByIdAsync(context.Client.ClientId);
                if (client != null)
                {
                    allowLocal = client.EnableLocalLogin;

                    if (client.IdentityProviderRestrictions != null && client.IdentityProviderRestrictions.Any())
                    {
                        providers = providers.Where(provider => client.IdentityProviderRestrictions.Contains(provider.AuthenticationScheme)).ToList();
                    }
                }
            }

            return new LoginViewModel
            {
                AllowRememberLogin = AccountOptions.AllowRememberLogin,
                EnableLocalLogin = allowLocal && AccountOptions.AllowLocalLogin,
                ReturnUrl = returnUrl,
                Username = context?.LoginHint,
                ExternalProviders = providers.ToArray()
            };
        }

        private async Task<LoginViewModel> BuildLoginViewModelAsync(LoginInputModel model)
        {
            var vm = await this.BuildLoginViewModelAsync(model.ReturnUrl);
            vm.Username = model.Username;
            vm.RememberLogin = model.RememberLogin;
            return vm;
        }

        private async Task<LogoutViewModel> BuildLogoutViewModelAsync(string logoutId)
        {
            var vm = new LogoutViewModel { LogoutId = logoutId, ShowLogoutPrompt = AccountOptions.ShowLogoutPrompt };

            if (User?.Identity.IsAuthenticated != true)
            {
                // if the user is not authenticated, then just show logged out page
                vm.ShowLogoutPrompt = false;
                return vm;
            }

            var context = await this.interaction.GetLogoutContextAsync(logoutId);
            if (context?.ShowSignoutPrompt == false)
            {
                // it's safe to automatically sign-out
                vm.ShowLogoutPrompt = false;
                return vm;
            }

            // show the logout prompt. this prevents attacks where the user
            // is automatically signed out by another malicious web page.
            return vm;
        }

        private async Task<LoggedOutViewModel> BuildLoggedOutViewModelAsync(string logoutId)
        {
            // get context information (client name, post logout redirect URI and iframe for federated signout)
            var logout = await this.interaction.GetLogoutContextAsync(logoutId);

            var vm = new LoggedOutViewModel
            {
                AutomaticRedirectAfterSignOut = AccountOptions.AutomaticRedirectAfterSignOut,
                PostLogoutRedirectUri = logout?.PostLogoutRedirectUri,
                ClientName = string.IsNullOrEmpty(logout?.ClientName) ? logout?.ClientId : logout?.ClientName,
                SignOutIframeUrl = logout?.SignOutIFrameUrl,
                LogoutId = logoutId
            };

            if (User?.Identity.IsAuthenticated == true)
            {
                var idp = User.FindFirst(JwtClaimTypes.IdentityProvider)?.Value;
                if (idp != null && idp != IdentityServer4.IdentityServerConstants.LocalIdentityProvider)
                {
                    var providerSupportsSignout = await HttpContext.GetSchemeSupportsSignOutAsync(idp);
                    if (providerSupportsSignout)
                    {
                        if (vm.LogoutId == null)
                        {
                            // if there's no current logout context, we need to create one
                            // this captures necessary info from the current logged in user
                            // before we signout and redirect away to the external IdP for signout
                            vm.LogoutId = await this.interaction.CreateLogoutContextAsync();
                        }

                        vm.ExternalAuthenticationScheme = idp;
                    }
                }
            }

            return vm;
        }

        //private ILogger GetLogger(string userId, string displayName, string email)
        //{
        //    string connectionString = this.Configuration["Serilog:ConnectionString"];
        //    string tableName = this.Configuration["Serilog:TableName"];
        //    string tableSchema = this.Configuration["Serilog:Schema"];
        //    return LoggerProvider.GetLoggerWithConfiguration(connectionString, tableName, tableSchema)
        //       .ForContext(ColumnNames.UserId, userId)
        //       .ForContext(ColumnNames.UserName, displayName)
        //       .ForContext(ColumnNames.UserEmail, email);
        //}
    }
}
