using System;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System.Net.Http;
using System.Collections.Generic;

namespace clientapp
{
    class Program
    {
        static void Main(string[] args)
        {
            #region parameters
            string authority,resource,deviceloginaddr;
            authority ="https://login.microsoftonline.com/common";
            resource="https://office365devlabs.onmicrosoft.com/TodoListService-OBO";
            deviceloginaddr="https://aka.ms/devicelogin";     
       
            #endregion

            var ctx = new AuthenticationContext(authority: authority);
            var deviceCode = ctx.AcquireDeviceCodeAsync(resource, "27e28dab-6cd7-4ae9-8d61-f8fc376df55f").Result;
            Console.WriteLine($"请打开浏览器，访问 {deviceloginaddr},并以 {deviceCode.UserCode} 登陆");
            var result = ctx.AcquireTokenByDeviceCodeAsync(deviceCode).Result;



            var httpClient =new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", result.AccessToken);

            // Call the To Do list service.
            HttpResponseMessage response = httpClient.GetAsync("http://localhost:5000/api/values").Result;
            Console.WriteLine(response.Content.ReadAsStringAsync().Result);



        }
    }
}
