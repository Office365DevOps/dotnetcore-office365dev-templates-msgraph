/*
作者：陈希章 Ares Chen
时间：2018年4月22日
说明：
    这是一个可以快速通过Microsoft Graph访问到Office 365资源的服务应用程序模板，通常理解为无人值守自动运行的程序。
    由于是一个无人值守的应用程序，所以不需要用户输入账号或者密码，该模板使用了应用程序密钥作为身份凭据。为了使用该应用程序，你需要得到Office 365管理员的授权确认。
    目前该模板同时支持国际版和国内版。

关于此模板的使用以及问题反馈，请访问 https://github.com/chenxizhang/dotnetcore-office365dev-templates/tree/master/dotnetcore-graph-serviceapp
Office 365开发入门指南，请参考 https://github.com/chenxizhang/office365dev 
更多模板请参考 https://github.com/chenxizhang/dotnetcore-office365dev-templates 
*/
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
            #region parameters
            string clientId,authority,adminconsent,resource,secret;
            clientId ="{{clientId}}";
            secret ="{{secret}}";

            //#if(instance=="global")
            //注册一个native app，申请应用程序权限，管理员确认，申请一个密钥

            adminconsent ="https://login.microsoftonline.com/common/adminconsent?client_id={{clientId}}&state=12345&redirect_uri=http://localhost";
            authority ="https://login.microsoftonline.com/{{tenantid}}/oauth2";
            resource="https://graph.microsoft.com";
            

            //#else
            adminconsent ="https://login.chinacloudapi.cn/common/adminconsent?client_id={{clientId}}&state=12345&redirect_uri=http://localhost";
            authority ="https://login.chinacloudapi.cn/{{tenantid}}/oauth2";
            resource="https://microsoftgraph.chinacloudapi.cn";
            //#endif
            #endregion

            Console.WriteLine($"欢迎使用该模板，请注意，服务应用申请的权限，需要Office 365管理员进行授权。如果你还没有授权，请访问 {adminconsent}");
            Console.WriteLine("该模板要求修改代码中的用户及邮箱信息，确认请按任意键继续，否则请取消(Ctrl+C)");
            Console.ReadKey();

            var token = "";
            var client = new GraphServiceClient(new DelegateAuthenticationProvider(async (request) =>
            {
                if (string.IsNullOrEmpty(token))
                {
                    var ctx = new AuthenticationContext(authority: authority);
                    token = (await ctx.AcquireTokenAsync(resource,new ClientCredential(clientId,secret))).AccessToken;
                }

                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
                await Task.FromResult(0);
            }))
            {
                BaseUrl = $"{resource}/{{version}}"
            };

            Console.WriteLine("读取所有用户基本信息");
            //读取所有用户信息
            var users = client.Users.Request().GetAsync().Result;
            foreach (var item in users)
            {
                Console.WriteLine($"\t{item.DisplayName}");
            }


            var testuserupn ="测试用户的upn，请修改";
            var emailtoaddress="邮件收件人邮箱，请修改";


            Console.WriteLine("\n读取某个用户的邮件列表（第一页）");
            
            //读取第一个用户的邮件
            var messages = client.Users[testuserupn].Messages.Request().GetAsync().Result;
            foreach (var item in messages)
            {
                Console.WriteLine($"\t{item.Subject}");
            }

            Console.WriteLine("\n读取某个用户的个人网盘");
            //读取第一个用户的个人网盘
            var files = client.Users[testuserupn].Drive.Root.Children.Request().GetAsync().Result;
            foreach (var item in files)
            {
                Console.WriteLine($"\t{item.Name}");
            }

            Console.WriteLine("\n代表某个用户发送一封测试邮件");
            //代表第一个用户发邮件
            var message =new Message(){
                Subject ="通过服务程序发出来的邮件",
                ToRecipients = new[]{
                    new Recipient(){
                        EmailAddress =new EmailAddress(){Address =emailtoaddress}
                    }
                }
            };
            
            client.Users[testuserupn].SendMail(message,true).Request().PostAsync().Wait();
            System.Console.WriteLine("\t发送成功");

            Console.WriteLine("\n代表某个用户上传一个文件到个人网盘");
            //上传一个文件到第一个用户的个人网盘
            using(var stream =new MemoryStream()){
                var content = $"Hello, {DateTime.Now.ToLongDateString()} ";
                var buffer = Encoding.UTF8.GetBytes(content);
                stream.Write(buffer,0,buffer.Length);
                stream.Position =0;

                client.Users[testuserupn].Drive.Root.ItemWithPath("test.txt").Content.Request().PutAsync<DriveItem>(stream).Wait();
                stream.Close();
                Console.WriteLine("\t上传成功");
            };


        }
    }
}
