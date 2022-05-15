using System.Net.Http.Headers;
using NS.WebApp.MVC.Extensions;
using Microsoft.Extensions.Primitives;

namespace NS.WebApp.MVC.Services.Handlers;

public class HttpClientAuthorizationDelegatingHandler : DelegatingHandler
{
    private readonly IUser _user;

    public HttpClientAuthorizationDelegatingHandler(IUser user)
    {
        _user = user;
    }

    protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        StringValues authorizationHeader = _user.ObterHttpContext().Request.Headers["Authorization"];

        if (!string.IsNullOrEmpty(authorizationHeader))
        {
            request.Headers.Add("Authorization", new List<string> { authorizationHeader });
        }

        string? token = _user.ObterUserToken();

        if (!string.IsNullOrWhiteSpace(token))
        {
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        return base.SendAsync(request, cancellationToken);
    }
}