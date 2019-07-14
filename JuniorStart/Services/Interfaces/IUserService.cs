using JuniorStart.Entities;

namespace JuniorStart.Services
{
    public interface IUserService
    {
        bool Create(User user, string password);
        User GetById(string id);
    }
}