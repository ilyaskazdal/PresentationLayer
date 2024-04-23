using BusinessLogicLayer.Abstract;
using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Concrete.EfCore
{
    public class EfUserRepo : IUserRepo
    {
        private ApplicationDbContext _context;

        public EfUserRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<User> Users =>  _context.Users;

        public void CreateNewUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public async Task<User> FindByIdAsync(int userId)
        {
            return await Users.FirstOrDefaultAsync(u => u.UserId == userId);
        }

        public User GetUserById(int id)
        {
            return _context.Users.FirstOrDefault(u => u.UserId == id);
        }
        public async Task<User> GetUserByUsername(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserNickName == username);
        }

        public void UpdateUser(User user)
        {
            _context.Entry(user).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            _context.SaveChanges();
        }


    }
}
