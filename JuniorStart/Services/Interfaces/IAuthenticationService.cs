using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.Extensions.Primitives;

namespace JuniorStart.Services.Interfaces
{
    public interface IAuthenticationService
    {
        string Authenticate(string username, string password);
        StringValues CreateToken(List<Claim> claims);
    }
}