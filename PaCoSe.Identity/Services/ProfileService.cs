using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Identity;
using PaCoSe.Caching;
using PaCoSe.Identity.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PaCoSe.Identity.Services
{
    public class ProfileService : IProfileService
    {
        private UserManager<AppUser> _userManager;

        private ICacheProvider _cacheProvider;

        public ProfileService(UserManager<AppUser> userManager, ICacheProvider cacheProvider)
        {
            this._userManager = userManager;
            this._cacheProvider = cacheProvider;
        }

        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            var user = await this.GetUserAsync(context.Subject);

            var claims = new List<Claim>
            {
                new Claim(JwtClaimTypes.Id, user.Id),
                new Claim(ClaimTypes.Email, user.Email ?? user.UserName),
                new Claim(JwtClaimTypes.PreferredUserName, user.UserName),
                new Claim(JwtClaimTypes.Name, user.DisplayName),
            };

            context.IssuedClaims.AddRange(claims);
        }

        public async Task IsActiveAsync(IsActiveContext context)
        {
            var user = await this.GetUserAsync(context.Subject);
            context.IsActive = user != null && (!user.LockoutEnabled || user.LockoutEnd == null || user.LockoutEnd.Value < DateTime.UtcNow);
        }

        private async Task<AppUser> GetUserAsync(ClaimsPrincipal subject)
        {
            var preferredUserName = subject.Claims.FirstOrDefault(c => c.Type == JwtClaimTypes.PreferredUserName)?.Value?.ToString();
            var cacheKey = $"IdentityUser:{preferredUserName}";
            var user = this._cacheProvider.Get<AppUser>(cacheKey);
            if (user == null)
            {
                user = await _userManager.GetUserAsync(subject);
                this._cacheProvider.AddOrUpdate(cacheKey, () => user, TimeSpan.FromDays(7));
            }

            return user;
        }
    }
}
