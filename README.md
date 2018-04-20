---
//国际版
// var ctx =new AuthenticationContext("https://login.microsoftonline.com/common/oauth2");
// var deviceCode = ctx.AcquireDeviceCodeAsync("https://graph.microsoft.com","cfc3a225-ac57-45c9-aacd-969551f4825f").Result;
// System.Console.WriteLine($"请打开浏览器，访问https://aka.ms/devicelogin,并以 {deviceCode.UserCode} 登陆");
// var token = ctx.AcquireTokenByDeviceCodeAsync(deviceCode).Result.AccessToken;
// request.Headers.Authorization = new AuthenticationHeaderValue("Bearer",token);

var ctx =new AuthenticationContext("https://login.chinacloudapi.cn/common/oauth2");
var deviceCode = ctx.AcquireDeviceCodeAsync("https://microsoftgraph.chinacloudapi.cn","d430823c-5613-4b1a-8d39-78f29c549f3e").Result;
System.Console.WriteLine($"请打开浏览器，访问 https://aka.ms/gallatindevicelogin,并以 {deviceCode.UserCode} 登陆");
var token = ctx.AcquireTokenByDeviceCodeAsync(deviceCode).Result.AccessToken;
Console.WriteLine(token);
request.Headers.Authorization = new AuthenticationHeaderValue("Bearer",token);

//reply url msalcfc3a225-ac57-45c9-aacd-969551f4825f://auth


dotnet nuget push .\chenxizhang.dotnetcore.msgraph.console.CSharp.1.0.0.nupkg -s https://www.nuget.org/api/v2/package
---
# dotnetcore-office365dev-templates