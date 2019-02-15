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

namespace webapi
{

    public class Startup
    {
        public static string clientid, authority, adminconsent, resource, secret, issuerSigningKey, tenantid, validissuer, validAudience;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            #region parameters
            clientid = "{{clientId}}";
            secret = "{{secret}}";
            tenantid = "{{tenantid}}";

            //#if(instance=="global")
            authority = "https://login.microsoftonline.com/common";
            resource = "https://graph.microsoft.com";
            validissuer = "https://sts.windows.net/{{tenantid}}/";
            validAudience = "https://office365devlabs.onmicrosoft.com/TodoListService-OBO";//请修改成自己的service

            //#else
            authority = "https://login.chinacloudapi.cn/common";
            resource = "https://microsoftgraph.chinacloudapi.cn";
            validissuer = "https://sts.chinacloudapi.cn/{{tenantid}}/";
            validAudience = "https://modtsp.partner.onmschina.cn/dotnet-obo-webapi";
            //#endif
            #endregion

        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(o =>
            {
                o.Authority = authority;
                o.Audience = validAudience;
                o.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = validissuer
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
