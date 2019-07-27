using System;
using System.Collections.Generic;
using System.Linq;
using JuniorStart.DTO;
using JuniorStart.Entities;
using JuniorStart.Factories;
using JuniorStart.Repository;
using JuniorStart.Services.Interfaces;

namespace JuniorStart.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationContext _context;
        private readonly IModelFactory _modelFactory;

        public UserService(ApplicationContext context, IModelFactory modelFactory)
        {
            _context = context;
            _modelFactory = modelFactory;
        }

        public bool Create(UserViewModel userViewModel)
        {
            if (_context.Users.FirstOrDefault(x => x.Login == userViewModel.Login) != null)
                throw new Exception("Username \"" + userViewModel.Login + "\" is already taken");
            
            User user = _modelFactory.Map(userViewModel);
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(user.Password, out passwordHash, out passwordSalt);
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _context.Users.Add(user);

            _context.SaveChanges();

            return true;
        }

        public User GetById(int id)
        {
            return _context.Users.FirstOrDefault(x => x.Id == id);
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