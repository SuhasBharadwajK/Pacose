using IdentityModel;
using Microsoft.AspNetCore.Http;
using PaCoSe.Caching;
using PaCoSe.Core.Contracts;
using PaCoSe.Exceptions;
using PaCoSe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

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

        private string UserName
        {
            get
            {
                return this.Claims.FirstOrDefault(c => c.Type == JwtClaimTypes.PreferredUserName)?.Value ?? this.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Upn)?.Value;
            }
        }

        private string DeviceName
        {
            get
            {
                return string.Empty; // TODO: Get the device name from the data in the header.
            }
        }

        private string CacheKey
        {
            get
            {
                return !string.IsNullOrEmpty(this.UserName) ? $"{CacheKeys.UserContext}:{this.UserName}" : $"{CacheKeys.DeviceContext}:{this.DeviceName}";
            }
        }

        private bool IsUser
        {
            get
            {
                return true; // TODO: Return true or false based on the header value
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

        public IRequestContext Build()
        {
            if (this.IsUser)
            {
                if (!string.IsNullOrEmpty(this.UserName) && this.HttpContextAccessor != null && this.HttpContextAccessor.HttpContext != null
                        && this.HttpContextAccessor.HttpContext.User != null
                        && this.HttpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
                {
                    var user = this.GetUser(this.UserName, this.CacheKey);
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
            else
            {
                return null;
            }

            throw new AccessDeniedException("The user does not have access");
        }

        public void Clear(string cacheKey = null)
        {
        }

        private User GetUser(string userName, string cacheKey)
        {
            return null;
        }

        private Device GetDevice(string deviceName, string cacheKey)
        {
            return null;
        }
    }
}
