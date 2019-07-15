using System;
using System.Collections.Generic;
using System.Linq;
using JuniorStart.Entities;
using JuniorStart.Repository;

namespace JuniorStart.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationContext _context;
        
        public UserService(ApplicationContext context)
        {
            _context = context;
        }
        
        public bool Create(User user)
        {

            if (_context.Users.FirstOrDefault(x => x.Login == user.Login) != null)
                throw new Exception("Username \"" + user.Login + "\" is already taken");

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(user.Password, out passwordHash, out passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _context.Users.Add(user);

             _context.SaveChanges();

            return true;
        }
        public User GetById(string login)
        {
            return _context.Users.FirstOrDefault(x => x.Login == login);
        }
        public IEnumerable<User> GetAll()
        {
            return _context.Users.ToList();
        }
        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA256())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}