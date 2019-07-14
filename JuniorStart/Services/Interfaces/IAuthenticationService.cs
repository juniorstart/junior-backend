using JuniorStart.Entities;

namespace JuniorStart.Services
{
    public interface IAuthenticationService
    {
        bool Authenticate(string username, string password);
    }
}