using System.Security.Claims;

namespace NS.WebApp.MVC.Extensions;

public interface IUser
{
    string Name { get; }
    Guid ObterUserId();
    string ObterUserEmail();
    string ObterUserToken();
    bool EstaAutenticado();
    bool PossuiRole(string role);
    IEnumerable<Claim> ObterClaims();
    HttpContext ObterHttpContext();
}

public class AspNetUser : IUser
{
    private readonly IHttpContextAccessor _accessor;

    public AspNetUser(IHttpContextAccessor accessor)
    {
        _accessor = accessor;
    }

    public string Name => _accessor.HttpContext?.User.Identity?.Name ?? string.Empty;

    public Guid ObterUserId()
    {
        return EstaAutenticado() ? Guid.Parse(_accessor.HttpContext?.User.GetUserId() ?? string.Empty) : Guid.Empty;
    }

    public string ObterUserEmail()
    {
        return (EstaAutenticado() ? _accessor.HttpContext?.User.GetUserEmail() : "") ?? string.Empty;
    }

    public string ObterUserToken()
    {
        return (EstaAutenticado() ? _accessor.HttpContext?.User.GetUserToken() : "") ?? string.Empty;
    }

    public bool EstaAutenticado()
    {
        return _accessor.HttpContext != null && _accessor.HttpContext.User.Identity!.IsAuthenticated;
    }

    public bool PossuiRole(string role)
    {
        return _accessor.HttpContext!.User.IsInRole(role);
    }

    public IEnumerable<Claim> ObterClaims()
    {
        return _accessor.HttpContext.User.Claims;
    }

    public HttpContext ObterHttpContext()
    {
        return _accessor.HttpContext;
    }
}

public static class ClaimsPrincipalExtensions
{
    public static string GetUserId(this ClaimsPrincipal principal)
    {
        if (principal == null)
        {
            throw new ArgumentException(nameof(principal));
        }

        Claim? claim = principal.FindFirst("sub");
        return claim?.Value;
    }

    public static string GetUserEmail(this ClaimsPrincipal principal)
    {
        if (principal == null)
        {
            throw new ArgumentException(nameof(principal));
        }

        Claim? claim = principal.FindFirst("email");
        return claim?.Value;
    }

    public static string GetUserToken(this ClaimsPrincipal principal)
    {
        if (principal == null)
        {
            throw new ArgumentException(nameof(principal));
        }

        Claim? claim = principal.FindFirst("JWT");
        return claim?.Value;
    }
}