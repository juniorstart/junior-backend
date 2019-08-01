using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using JuniorStart.Services.Interfaces;
using Microsoft.AspNetCore.Http;

namespace JuniorStart.Middlewares
{
    public class JwtTokenSlidingExpirationMiddleware
    {
        private readonly RequestDelegate _next;
        public JwtTokenSlidingExpirationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext context, IAuthenticationService _authenticationService)
        {
            string authorization = context.Request.Headers["Authorization"].FirstOrDefault();
            if (authorization == null || !authorization.ToLower().StartsWith("bearer") || string.IsNullOrWhiteSpace(authorization.Substring(6)))
            {
                return _next(context);
            }
            
            ClaimsPrincipal claimsPrincipal = context.Request.HttpContext.User;
            if (claimsPrincipal == null || !claimsPrincipal.Identity.IsAuthenticated)
            {
                return _next(context);
            }

            // Extract the claims and put them into a new JWT
            context.Response.Headers.Add("Set-Authorization", _authenticationService.CreateToken(claimsPrincipal.Claims.FirstOrDefault()));
            
            return _next(context);
        }
    }
}