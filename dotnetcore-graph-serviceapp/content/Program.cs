using System;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Graph;
using System.Threading.Tasks;
using System.Net.Http.Headers;
using System.Linq;
using System.IO;
using System.Text;

namespace content
{
    class Program
    {
        static  void Main(string[] args)
        {
            //注册一个native app，申请应用程序权限，管理员确认，申请一个密钥
            var clientId ="019dc431-2ad8-4b8e-a141-499f1ebcf737";
            var adminconsent ="https://login.microsoftonline.com/common/adminconsent?client_id=019dc431-2ad8-4b8e-a141-499f1ebcf737&state=12345&redirect_uri=http://localhost";
            var authority ="https://login.microsoftonline.com/59723f6b-2d14-49fe-827a-8d04f9fe7a68/oauth2";
            var resource="https://graph.microsoft.com";
            var secret ="wvfLJHK456#}oadnKTG16{{";
            var tenant ="59723f6b-2d14-49fe-827a-8d04f9fe7a68";

            var token = "";

            var client = new GraphServiceClient(new DelegateAuthenticationProvider(async (request) =>
            {
                if (string.IsNullOrEmpty(token))
                {
                    var ctx = new AuthenticationContext(authority: authority);
                    token = (await ctx.AcquireTokenAsync(resource,new ClientCredential(clientId,secret))).AccessToken;
                }

                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                await Task.FromResult(1);
            }))
            {
                BaseUrl = $"{resource}/v1.0"
            };


            //读取所有用户信息
            var users = client.Users.Request().GetAsync().Result;
            foreach (var item in users)
            {
                Console.WriteLine(item.DisplayName);
            }

            //读取第一个用户的邮件
            var messages = client.Users["ares@office365devlabs.onmicrosoft.com"].Messages.Request().GetAsync().Result;
            foreach (var item in messages)
            {
                Console.WriteLine(item.Subject);
            }


            //读取第一个用户的个人网盘
            var files = client.Users["ares@office365devlabs.onmicrosoft.com"].Drive.Root.Children.Request().GetAsync().Result;
            foreach (var item in files)
            {
                Console.WriteLine(item.Name);
            }

            //代表第一个用户发邮件
            var message =new Message(){
                Subject ="通过服务程序发出来的邮件",
                ToRecipients = new[]{
                    new Recipient(){
                        EmailAddress =new EmailAddress(){Address ="ares@xizhang.com"}
                    }
                }
            };
            
            client.Users["ares@office365devlabs.onmicrosoft.com"].SendMail(message,true).Request().PostAsync().Wait();


            //上传一个文件到第一个用户的个人网盘
            using(var stream =new MemoryStream()){
                var content = $"Hello, {DateTime.Now.ToLongDateString()} ";
                var buffer = Encoding.UTF8.GetBytes(content);
                stream.Write(buffer,0,buffer.Length);
                stream.Position =0;

                client.Users["ares@office365devlabs.onmicrosoft.com"].Drive.Root.ItemWithPath("test.txt").Content.Request().PutAsync<DriveItem>(stream).Wait();
                stream.Close();
            };


        }
    }
}
