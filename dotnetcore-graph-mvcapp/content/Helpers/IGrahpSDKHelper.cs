using Microsoft.AspNetCore.Http;
using Microsoft.Graph;
public interface IGraphSDKHelper
{
    GraphServiceClient GetServiceClient(string identifier,HttpContext context);
}