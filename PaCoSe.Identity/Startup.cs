using IdentityServer4;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using PaCoSe.Core.Contracts;
using PaCoSe.Core.Providers;
using PaCoSe.Identity.Configuration;
using PaCoSe.Identity.Core;
using PaCoSe.Identity.DbContext;
using PaCoSe.Identity.Extensions;
using PaCoSe.Identity.Services;
using System.Reflection;

namespace PaCoSe.Identity
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews(); string connectionString = this.Configuration["ConnectionStrings:DbConnection"];
            var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            services.AddIdentity<AppUser, AppRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddIdentityServer()
                .AddInMemoryClients(Clients.Get())
                .AddInMemoryIdentityResources(Resources.GetIdentityResources())
                .AddInMemoryApiResources(Resources.GetApiResources())
                // TODO: Uncommment the below lines when SQL server for identity is implemented.
                //.AddOperationalStore(options => options.ConfigureDbContext = builder => builder.UseSqlServer(connectionString, sqlOptions => sqlOptions.MigrationsAssembly(migrationsAssembly)))
                //.AddConfigurationStore(options => options.ConfigureDbContext = builder => builder.UseSqlServer(connectionString, sqlOptions => sqlOptions.MigrationsAssembly(migrationsAssembly)))
                .AddAspNetIdentity<AppUser>()
                .LoadSigningCredentialFrom(this.Configuration["Certificate:Thumbprint"]);

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie("cookie")
                .AddOpenIdConnect(
                "Office365",
                options =>
                {
                    options.SignOutScheme = IdentityServerConstants.SignoutScheme;

                    options.ClientId = this.Configuration["AuthProviders:Office365:ClientId"];
                    options.Authority = this.Configuration["AuthProviders:Office365:Authority"];

                    options.Scope.Clear();
                    options.Scope.Add("openid");
                    options.Scope.Add("profile");
                    options.Scope.Add("email");
                    options.Scope.Add("User.Read");
                    options.Scope.Add("User.ReadBasic.All");
                    options.ReturnUrlParameter = "/Account/ExternalLoginCallback";
                    options.ResponseType = OpenIdConnectResponseType.IdToken;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        NameClaimType = "email",
                        RoleClaimType = "role",
                        ValidateIssuer = false,
                    };
                });

            services.AddCors(options => options.AddPolicy(
                "AllowAll",
                p =>
                    //// TO DO: Restrict origins based on configuration from DB.
                    p.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()));

            services.AddDbContext<ApplicationDbContext>(builder => builder.UseSqlServer(connectionString, sqlOptions => sqlOptions.MigrationsAssembly(migrationsAssembly)));

            services.AddScoped<ICoreDataContract, CoreDataProvider>();

            services.RegisterDatabaseProviders(this.Configuration["ConnectionStrings:DbConnection"], this.Configuration["ConnectionStrings:Provider"]);
            services.RegisterCacheProviders(bool.Parse(this.Configuration["Redis:IsEnabled"]), this.Configuration["Redis:ConnectionString"], int.Parse(this.Configuration["Redis:DefaultTimeout"]));
            services.RegisterMappingProfiles();

            services.AddTransient<IProfileService, ProfileService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseCors("AllowAll");

            app.UseIdentityServer();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
