﻿using WebRecipesBE.Data;
using WebRecipesBE.Interfaces;
using WebRecipesBE.Models;

namespace WebRecipesBE.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DataContext _context;

        public UserRepository(DataContext context)
        {
            _context = context;
        }
        public bool CreateUser(User user)
        {
            _context.Add(user);
            return Save();
        }

        public bool DeleteUser(User user)
        {
            _context.Remove(user);
            return Save();
        }

        public ICollection<User> GetAllUsers()
        {
            return _context.Users.OrderBy(u => u.id_Vartotojas).ToList();
        }

        public User GetUser(int id)
        {
            return _context.Users.Where(u => u.id_Vartotojas == id).FirstOrDefault();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateUser(User user)
        {
            _context.Update(user);
            return Save();
        }

        public bool UserExists(int id)
        {
            return _context.Users.Any(r => r.id_Vartotojas == id);
        }

        public User Authenticate(string email, string password)
        {
            return _context.Users.FirstOrDefault(u => u.email == email && u.password == password);
        }
    }
}
