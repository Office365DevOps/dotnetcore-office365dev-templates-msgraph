using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Graph;
using System.Net.Http.Headers;
using System.Text;

namespace content
{
   public class Startup
    {

        public  string authority ="https://login.microsoftonline.com/common";
        public  string resource ="https://graph.microsoft.com";
        public  string clientid ="5200f0d2-0ab3-4cc4-bb13-3506a04106e0";
        public  string secret ="fthpNSD50~#ppuWHNE130-+";

        public  string ObjectIdentifierType = "http://schemas.microsoft.com/identity/claims/objectidentifier";


        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddMemoryCache();

            services.AddAuthentication(sharedOptions =>
            {
                sharedOptions.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                sharedOptions.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                sharedOptions.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
                .AddOpenIdConnect(options =>{
                    options.Authority = authority;
                    options.Resource = resource;
                    options.ClientId = clientid;
                    options.ClientSecret =secret;
                    options.ResponseType = OpenIdConnectResponseType.CodeIdToken;
                    options.TokenValidationParameters = new TokenValidationParameters{
                        ValidateIssuer =false
                    };
                    options.Events = new OpenIdConnectEvents{
                        OnTicketReceived =context =>{
                            return Task.CompletedTask;
                        },
                        OnAuthenticationFailed = async (context) =>{
                            await context.Response.WriteAsync("Fail");
                        },
                        OnAuthorizationCodeReceived = async(context)=>{
                            var code = context.ProtocolMessage.Code;

                            var memorycache = context.HttpContext.RequestServices.GetRequiredService<IMemoryCache>();
                            var identifier = context.Principal.FindFirst(ObjectIdentifierType).Value;
                            var sessionTokencache = new SessionTokenCache(identifier,memorycache);

                            var ctx = new AuthenticationContext(authority,sessionTokencache.GetCacheInstance());
                            var result = await ctx.AcquireTokenByAuthorizationCodeAsync(code,new Uri("http://localhost:5000/signin-oidc"),new ClientCredential(clientid,secret));
                            context.HandleCodeRedemption(result.AccessToken,result.IdToken);
                            
                        }
                    };
                })
                .AddCookie();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();

            app.Map("/test",builder=>{
                
                builder.Run(async (context)=>{
                    await context.Response.WriteAsync("中文");
                });
            });

            app.Run(async (context) =>
            {

                if(context.User.Identity.IsAuthenticated){

                    var identifier = context.User.FindFirst(ObjectIdentifierType).Value;
                    var memorycache = context.RequestServices.GetRequiredService<IMemoryCache>();
                    var sessionTokencache = new SessionTokenCache(identifier,memorycache);
                    var ctx = new AuthenticationContext(authority,sessionTokencache.GetCacheInstance());
                    var result =await ctx.AcquireTokenSilentAsync(resource,new ClientCredential(clientid,secret),new UserIdentifier(identifier,UserIdentifierType.UniqueId));
                    
                    var graphserviceClient = new GraphServiceClient(new DelegateAuthenticationProvider(async(request) => {
                        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", result.AccessToken);
                        await Task.FromResult(0);
                    }));


                    var me =await graphserviceClient.Me.Request().GetAsync();
                    await context.Response.WriteAsync(me.DisplayName,Encoding.UTF8);
                }
                else{
                    await AuthenticationHttpContextExtensions.ChallengeAsync(context,OpenIdConnectDefaults.AuthenticationScheme);
                }
            });
        }
    }
}
