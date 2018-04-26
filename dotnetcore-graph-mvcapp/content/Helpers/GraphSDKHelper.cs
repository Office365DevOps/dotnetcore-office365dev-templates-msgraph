using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Graph;
using Microsoft.IdentityModel.Clients.ActiveDirectory;


public class GraphSDKHelper : IGraphSDKHelper
{
    private GraphSettings settings;
    public GraphSDKHelper(GraphSettings _settings)
    {
        settings = _settings;
    }
    public GraphServiceClient GetServiceClient(string identifier, HttpContext context)
    {
        var memorycache = context.RequestServices.GetRequiredService<IMemoryCache>();
        var sessionTokencache = new SessionTokenCache(identifier, memorycache);
        var ctx = new AuthenticationContext(settings.Authority, sessionTokencache.GetCacheInstance());
        var result = ctx.AcquireTokenSilentAsync(settings.Resource, new ClientCredential(settings.ClientId, settings.Secret), new UserIdentifier(identifier, UserIdentifierType.UniqueId)).Result;

        var graphserviceClient = new GraphServiceClient(new DelegateAuthenticationProvider(async (request) =>
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", result.AccessToken);
            await Task.FromResult(0);
        }))
        {
            BaseUrl = $"{settings.Resource}/{settings.Version}"
        };

        return graphserviceClient;
    }
}