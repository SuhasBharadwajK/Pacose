using System;

namespace PaCoSe.Identity.Models
{
    public static class AccountOptions
    {
        public static bool AllowLocalLogin { get; } = true;

        public static bool AllowRememberLogin { get; } = true;

        public static TimeSpan RememberMeLoginDuration { get; } = TimeSpan.FromDays(30);

        public static bool ShowLogoutPrompt { get; } = false;

        public static bool AutomaticRedirectAfterSignOut { get; } = true;

        public static string WindowsAuthenticationSchemeName { get; } = Microsoft.AspNetCore.Server.IISIntegration.IISDefaults.AuthenticationScheme;

        public static bool IncludeWindowsGroups { get; } = false;

        public static string InvalidCredentialsErrorMessage { get; } = "Invalid username or password";
    }
}
