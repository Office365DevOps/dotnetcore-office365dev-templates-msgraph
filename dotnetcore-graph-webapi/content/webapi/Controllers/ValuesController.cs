using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Graph;
using System.Net.Http.Headers;

namespace content.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        //在这个方法中获取用户的基本信息，邮件信息，文件列表信息
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            StringValues token;
            if(Request.Headers.TryGetValue("Authorization",out token)){

                var accesstoken = token.First().Split(' ')[1];
                var userAssertion = new UserAssertion(accesstoken, "urn:ietf:params:oauth:grant-type:jwt-bearer", User.Identity.Name);
                var clientCred = new ClientCredential("653052ff-3dc1-4de3-9bee-ccc268a7cbfa", "fS1b8Ba3/0L37d8veyNmifFN1MoW+yXzBvfFBW1QXj4=");

                var authority ="https://login.microsoftonline.com/common";
                var ctx = new AuthenticationContext(authority);
                var result = ctx.AcquireTokenAsync("https://graph.microsoft.com",clientCred,userAssertion).Result;

                var client = new GraphServiceClient(new DelegateAuthenticationProvider(async request=>{
                    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", result.AccessToken);
                    await Task.FromResult(0);
                }));

                var me = client.Me.Request().GetAsync().Result;
                return new[]{me.DisplayName,me.UserPrincipalName,me.Department};

            }

            return new[]{"Hello,dotnet web api"};
        }
    }
}
