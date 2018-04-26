using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;

namespace content
{
    public class Startup
    {

        string clientid, authority, resource, secret, version;
        public const string ObjectIdentifierType = "http://schemas.microsoft.com/identity/claims/objectidentifier";
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            #region parameters

            clientid = "{{clientId}}";
            secret = "{{secret}}";
            version = "{{version}}";

            //#if(instance=="global")
            authority = "https://login.microsoftonline.com/common";
            resource = "https://graph.microsoft.com";


            //#else
            authority = "https://login.chinacloudapi.cn/common";
            resource = "https://microsoftgraph.chinacloudapi.cn";
            //#endif
            #endregion

        }

        public IConfiguration Configuration { get; }
        public const string TenantIdType = "http://schemas.microsoft.com/identity/claims/tenantid";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(sharedOptions =>
            {
                sharedOptions.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                sharedOptions.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                sharedOptions.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
            .AddOpenIdConnect(options =>
                {
                    options.Authority = authority;
                    options.Resource = resource;
                    options.ClientId = clientid;
                    options.ClientSecret = secret;
                    options.ResponseType = OpenIdConnectResponseType.CodeIdToken;
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false
                    };
                    options.Events = new OpenIdConnectEvents
                    {
                        OnTicketReceived = context =>
                        {
                            return Task.CompletedTask;
                        },
                        OnAuthenticationFailed = async (context) =>
                        {
                            await context.Response.WriteAsync("Fail");
                        },
                        OnAuthorizationCodeReceived = async (context) =>
                        {
                            var code = context.ProtocolMessage.Code;

                            var memorycache = context.HttpContext.RequestServices.GetRequiredService<IMemoryCache>();
                            var identifier = context.Principal.FindFirst(ObjectIdentifierType).Value;
                            var sessionTokencache = new SessionTokenCache(identifier, memorycache);

                            var ctx = new AuthenticationContext(authority, sessionTokencache.GetCacheInstance());
                            var result = await ctx.AcquireTokenByAuthorizationCodeAsync(code, new Uri("http://localhost:5000/signin-oidc"), new ClientCredential(clientid, secret));
                            context.HandleCodeRedemption(result.AccessToken, result.IdToken);

                        }
                    };
                })
                .AddCookie();

            services.AddMvc();

            // This sample uses an in-memory cache for tokens and subscriptions. Production apps will typically use some method of persistent storage.
            services.AddMemoryCache();
            services.AddSession();

            services.AddSingleton<IGraphSDKHelper, GraphSDKHelper>((provider) =>
            {
                return new GraphSDKHelper(new GraphSettings()
                {
                    Version = version,
                    ClientId = clientid,
                    Secret = secret,
                    Authority = authority,
                    Resource = resource
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();



            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseSession();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
