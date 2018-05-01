using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Http;
using System.Security.Cryptography.X509Certificates;

namespace content
{
    public class Startup
    {
        public static string clientid, authority, adminconsent, resource, secret,issuerSigningKey,tenantid,validissuer,validAudience;
        
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            #region parameters
            clientid = "{{clientId}}";
            secret = "{{secret}}";
            tenantid ="{{tenantid}}";

            //#if(instance=="global")
            authority ="https://login.microsoftonline.com/common";
            resource="https://graph.microsoft.com";
            issuerSigningKey ="MIIDBTCCAe2gAwIBAgIQev76BWqjWZxChmKkGqoAfDANBgkqhkiG9w0BAQsFADAtMSswKQYDVQQDEyJhY2NvdW50cy5hY2Nlc3Njb250cm9sLndpbmRvd3MubmV0MB4XDTE4MDIxODAwMDAwMFoXDTIwMDIxOTAwMDAwMFowLTErMCkGA1UEAxMiYWNjb3VudHMuYWNjZXNzY29udHJvbC53aW5kb3dzLm5ldDCCASIwDQYJKoZIhvcNAQEBBQADggEPADCCAQoCggEBAMgmGiRfLh6Fdi99XI2VA3XKHStWNRLEy5Aw/gxFxchnh2kPdk/bejFOs2swcx7yUWqxujjCNRsLBcWfaKUlTnrkY7i9x9noZlMrijgJy/Lk+HH5HX24PQCDf+twjnHHxZ9G6/8VLM2e5ZBeZm+t7M3vhuumEHG3UwloLF6cUeuPdW+exnOB1U1fHBIFOG8ns4SSIoq6zw5rdt0CSI6+l7b1DEjVvPLtJF+zyjlJ1Qp7NgBvAwdiPiRMU4l8IRVbuSVKoKYJoyJ4L3eXsjczoBSTJ6VjV2mygz96DC70MY3avccFrk7tCEC6ZlMRBfY1XPLyldT7tsR3EuzjecSa1M8CAwEAAaMhMB8wHQYDVR0OBBYEFIks1srixjpSLXeiR8zES5cTY6fBMA0GCSqGSIb3DQEBCwUAA4IBAQCKthfK4C31DMuDyQZVS3F7+4Evld3hjiwqu2uGDK+qFZas/D/eDunxsFpiwqC01RIMFFN8yvmMjHphLHiBHWxcBTS+tm7AhmAvWMdxO5lzJLS+UWAyPF5ICROe8Mu9iNJiO5JlCo0Wpui9RbB1C81Xhax1gWHK245ESL6k7YWvyMYWrGqr1NuQcNS0B/AIT1Nsj1WY7efMJQOmnMHkPUTWryVZlthijYyd7P2Gz6rY5a81DAFqhDNJl2pGIAE6HWtSzeUEh3jCsHEkoglKfm4VrGJEuXcALmfCMbdfTvtu4rlsaP2hQad+MG/KJFlenoTK34EMHeBPDCpqNDz8UVNk";
            validissuer ="https://sts.windows.net/{{tenantid}}/";
            validAudience ="https://office365devlabs.onmicrosoft.com/TodoListService-OBO";//请修改成自己的service
            
            //#else
            authority ="https://login.chinacloudapi.cn/common";
            resource="https://microsoftgraph.chinacloudapi.cn";
            issuerSigningKey ="MIIDDzCCAfegAwIBAgIQHzmbPTAQWqRFf9jLcIOyCTANBgkqhkiG9w0BAQsFADAyMTAwLgYDVQQDEydhY2NvdW50cy5hY2Nlc3Njb250cm9sLmNoaW5hY2xvdWRhcGkuY24wHhcNMTgwMjE4MDAwMDAwWhcNMjAwMjE5MDAwMDAwWjAyMTAwLgYDVQQDEydhY2NvdW50cy5hY2Nlc3Njb250cm9sLmNoaW5hY2xvdWRhcGkuY24wggEiMA0GCSqGSIb3DQEBAQUAA4IBDwAwggEKAoIBAQDAm1V69tmZhhkDgqlwubm8JWM/xQow+xGl50wTfu1qv7tUIShgY5OYj2uMBf5NWaWrdV7BBxhZoI/2kS4ux+kB3JSuqJOCS3LUWhzN9siBNlNOZOnpLdGI2tUGGnWzjx3KgOcNV0ZACHj/7oE9Qar+81BnUfdSE70TQ36Ovyp4tzY7r2UbSfi7jW2/l/1cney8kiIDZItMDGcrl4ZvhTzKaU54/wKq5AeA5bbeatLsGna+33GYhFJ1zpNhZrTF3dRxijMobvKtJxSNGeLYC99IZERDlAoiCqIt/Q967E1azuxkX9ti1h0acH0jLFzn7fv5nR0MZ4kv/5i/TWz5PouHAgMBAAGjITAfMB0GA1UdDgQWBBRkaPSMKZal9IEaTOcczTRHvq7o5jANBgkqhkiG9w0BAQsFAAOCAQEAU8wkH7uIyriSijnJur3UMIf2k4mIe7TjxsfdI/15RzRYpQKnMeH6GpnCOffLU29hUj8zx6yHlJ+ZdGmEERqE9sLZ5j1gZr4xlsHWkBEbHMsdTtOaJbGXrD9eZND8o1AAk9Vi/8ax7Icen8XDldSwZzcE35JDq74ROcL8ec1tUqsUSTnB7mjAOKqNLI0Zhc3lA/yl3QuW9iboebsNXfp3wtSMGeqPL5qPsKPIondJCTiFV3My06y2AkNPc+I8V8ZYMyrJ/AdtXGLyDWXi1rDhxfaurtZP10XrTCuNR6WQx8BdmXSqO/stGClx9NkbTNWxKz1rxgMcsnqLn7NYgLazxA==";
            validissuer ="https://sts.chinacloudapi.cn/{{tenantid}}/";
            validAudience ="https://modtsp.partner.onmschina.cn/dotnet-obo-webapi";
            //#endif
            #endregion

        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                SaveSigninToken =true,
                ValidAudience =validAudience,
                ValidIssuer = validissuer,
                IssuerSigningKey =new X509SecurityKey(new X509Certificate2(Convert.FromBase64String(issuerSigningKey)))
            };
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                o.IncludeErrorDetails = true;
                o.TokenValidationParameters = tokenValidationParameters;
                o.Events = new JwtBearerEvents()
                {
                    OnAuthenticationFailed = c =>
                    {
                        c.NoResult();

                        c.Response.StatusCode = 401;
                        c.Response.ContentType = "text/plain";

                        return c.Response.WriteAsync(c.Exception.ToString());
                    }

                };
            });
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
