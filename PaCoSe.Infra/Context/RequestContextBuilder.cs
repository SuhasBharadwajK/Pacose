using IdentityModel;
using Microsoft.AspNetCore.Http;
using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using PaCoSe.Caching;
using PaCoSe.Core.Contracts;
using PaCoSe.Exceptions;
using PaCoSe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace PaCoSe.Infra.Context
{
    public class RequestContextBuilder
    {
        private ICacheProvider CacheProvider { get; set; }

        private IHttpContextAccessor HttpContextAccessor { get; set; }

        private ICoreDataContract CoreDataContract { get; set; }

        private List<Claim> Claims
        {
            get
            {
                return this.HttpContextAccessor.HttpContext.User.Claims.ToList();
            }
        }

        private string Username
        {
            get
            {
                return this.Claims.FirstOrDefault(c => c.Type == JwtClaimTypes.PreferredUserName)?.Value ?? this.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Upn)?.Value;
            }
        }

        private string DeviceToken
        {
            get
            {
                if (this.HttpContextAccessor.HttpContext.Request.Headers.ContainsKey(HeaderNames.Authorization)
                        && this.HttpContextAccessor.HttpContext.Request.Headers[HeaderNames.Authorization].ToString().ToLower().StartsWith("device"))
                {
                    // Return the token from the value of the Authorization header
                    var encodedToken = this.HttpContextAccessor.HttpContext.Request.Headers[HeaderNames.Authorization].ToString().Split(' ').Last().Trim();
                    var base64EncodedBytes = Convert.FromBase64String(encodedToken);
                    var tokenData = JsonConvert.DeserializeObject<EncodedToken>(System.Text.Encoding.UTF8.GetString(base64EncodedBytes));
                    if (DateTime.Parse(tokenData.ValidTill) < DateTime.UtcNow)
                    {
                        throw new AccessDeniedException("The device token is expired");
                    }

                    return tokenData.TokenString;
                }

                throw new AccessDeniedException("The device token doesn't exist or is invalid");
            }
        }

        private string CacheKey
        {
            get
            {
                return !string.IsNullOrEmpty(this.Username) ? $"{CacheKeys.UserContext}:{this.Username}" : $"{CacheKeys.DeviceContext}:{this.DeviceToken}";
            }
        }

        private bool IsUser
        {
            get
            {
                return this.HttpContextAccessor.HttpContext.Request.Headers.ContainsKey(HeaderNames.Authorization)
                        && this.HttpContextAccessor.HttpContext.Request.Headers[HeaderNames.Authorization].ToString().ToLower().StartsWith("bearer");
            }
        }

        public RequestContextBuilder(
            ICacheProvider cacheProvider,
            IHttpContextAccessor httpContextAccessor,
            ICoreDataContract coreDataContract
        )
        {
            this.CacheProvider = cacheProvider;
            this.HttpContextAccessor = httpContextAccessor;
            this.CoreDataContract = coreDataContract;
        }

        public IRequestContext Build(bool isSystem = false, string contextUserName = null)
        {
            if (isSystem)
            {
                var userName = contextUserName ?? "System";
                return new BaseRequestContext(this)
                {
                    User = new User
                    {
                        Id = -1,
                        Username = userName,
                        UserProfile = new UserProfile
                        {
                            Email = userName,
                            FirstName = userName,
                            Id = -1,
                            UserId = -1,
                        }
                    }
                };
            }

            if (this.IsUser)
            {
                if (!string.IsNullOrEmpty(this.Username) && this.HttpContextAccessor != null && this.HttpContextAccessor.HttpContext != null
                        && this.HttpContextAccessor.HttpContext.User != null
                        && this.HttpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
                {
                    var user = this.GetUser(this.Username, this.CacheKey);
                    if (user == null)
                    {
                        throw new AccessDeniedException("The user does not have access");
                    }

                    return new BaseRequestContext(this)
                    {
                        User = user
                    };
                }
            }
            else if (!string.IsNullOrEmpty(this.Username) && this.HttpContextAccessor != null && this.HttpContextAccessor.HttpContext != null
                        && !string.IsNullOrEmpty(this.DeviceToken))
            {
                var device = this.GetDevice(this.DeviceToken, this.CacheKey);
                if (device == null)
                {
                    throw new AccessDeniedException("The device does not have access");
                }

                return new BaseRequestContext(this)
                {
                    Device = device
                };
            }

            throw new AccessDeniedException("Access is denied");
        }

        public void Clear(string cacheKey = null)
        {
            this.CacheProvider.Remove(cacheKey ?? this.CacheKey);
        }

        private User GetUser(string userName, string cacheKey)
        {
            var user = this.CacheProvider.Get<User>(cacheKey);
            if (user != null)
            {
                return user;
            }

            user = this.CoreDataContract.GetUser(userName);
            this.CacheProvider.AddOrUpdate<User>(cacheKey, () => user, TimeSpan.FromDays(3)); // Cache the user data for three days.

            return user;
        }

        private Device GetDevice(string deviceToken, string cacheKey)
        {
            var device = this.CacheProvider.Get<Device>(cacheKey);
            if (device != null)
            {
                return device;
            }

            device = this.CoreDataContract.GetDevice(deviceToken);
            this.CacheProvider.AddOrUpdate<Device>(cacheKey, () => device, TimeSpan.FromDays(7)); // Cache the device data for seven days.

            return device;
        }
    }
}
