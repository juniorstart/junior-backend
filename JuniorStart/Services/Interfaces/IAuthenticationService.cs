using JuniorStart.Entities;

namespace JuniorStart.Services
{
    public interface IAuthenticationService
    {
        string Authenticate(string username, string password);
    }
}