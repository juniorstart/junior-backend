using JuniorStart.DTO;
using JuniorStart.Entities;

namespace JuniorStart.Services.Interfaces
{
    public interface IUserService
    {
        bool Create(UserDto user);
        User Get(int id);
    }
}