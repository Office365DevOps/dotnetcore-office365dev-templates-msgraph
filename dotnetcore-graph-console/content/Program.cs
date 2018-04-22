using System;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Graph;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace content
{
    class Program
    {
        static void Main(string[] args)
        {
            #region parameters
            string authority,resource,deviceloginaddr;
            //#if(instance=="gallatin")
            authority ="https://login.chinacloudapi.cn/common/oauth2";
            resource ="https://microsoftgraph.chinacloudapi.cn";
            deviceloginaddr="https://aka.ms/gallatindevicelogin";
            
            //#else
            authority ="https://login.microsoftonline.com/common/oauth2";
            resource="https://graph.microsoft.com";
            deviceloginaddr="https://aka.ms/devicelogin";     
       
            //#endif
            #endregion



            var token = "";

            var client = new GraphServiceClient(new DelegateAuthenticationProvider((request) =>
            {
                if (string.IsNullOrEmpty(token))
                {
                    var ctx = new AuthenticationContext(authority: authority);
                    var deviceCode = ctx.AcquireDeviceCodeAsync(resource, "{{clientId}}").Result;
                    Console.WriteLine($"请打开浏览器，访问{deviceloginaddr},并以 {deviceCode.UserCode} 登陆");
                    token = ctx.AcquireTokenByDeviceCodeAsync(deviceCode).Result.AccessToken;
                }

                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                return Task.FromResult(0);
            }))
            {
                BaseUrl = $"{resource}/{{version}}"
            };

            var user = client.Me.Request().GetAsync().Result;
            Console.WriteLine($"当前登录用户为:{user.DisplayName}\r\n");

            var messages = client.Me.Messages.Request().GetAsync().Result;
            Console.WriteLine($"当前用户的收件箱最近十封邮件如下：");
            foreach (var item in messages)
            {
                Console.WriteLine($"\t{item.Subject}");
            }

            var files = client.Me.Drive.Root.Children.Request().GetAsync().Result;
            Console.WriteLine($"\r\n当前用户的个人网盘的文件信息如下：");
            foreach (var item in files)
            {
                Console.WriteLine($"\t{item.Name}");
            }

        }
    }
}
