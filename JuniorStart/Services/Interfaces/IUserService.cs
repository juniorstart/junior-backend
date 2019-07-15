using JuniorStart.Entities;

namespace JuniorStart.Services
{
    public interface IUserService
    {
        bool Create(User user);
        User GetById(string id);
    }
}