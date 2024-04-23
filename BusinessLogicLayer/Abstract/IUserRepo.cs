using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Abstract
{
    public interface IUserRepo
    {
        IQueryable<User> Users { get; }

        void CreateNewUser(User user);

        User GetUserById(int id);
        Task<User> GetUserByUsername(string username);

        Task<User> FindByIdAsync(int userId);


        void UpdateUser(User user);
    }
}
