using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using DotNetTest.Services;
using DotNetTest.Models;

namespace DotNetTest.Attributes;

public class AuthTokenRequired : TypeFilterAttribute
{
    public AuthTokenRequired() : base(typeof(TokenAuthorizationFilter))
    {
    }
}

public class TokenAuthorizationFilter : Attribute, IAsyncAuthorizationFilter
{
    private readonly IAuthTokenService<AuthorizedUser> _authTokenService;

    public TokenAuthorizationFilter(IAuthTokenService<AuthorizedUser> authTokenService)
    {
        _authTokenService = authTokenService;
    }
    public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var authorizationHeader = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault();
        if (string.IsNullOrEmpty(authorizationHeader))
        {
            throw new UnauthorizedAccessException("Authorization header is missing.");
        }

        string token = authorizationHeader.Replace("Bearer ", "");

        AuthorizedUser user = await _authTokenService.ValidateTokenAsync(token);
        context.HttpContext.Items["User"] = user;
    }
}