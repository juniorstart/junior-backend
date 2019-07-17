namespace JuniorStart.Services.Interfaces
{
    public interface IAuthenticationService
    {
        string Authenticate(string username, string password);
    }
}