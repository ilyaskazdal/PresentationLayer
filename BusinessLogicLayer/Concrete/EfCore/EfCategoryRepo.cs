using BusinessLogicLayer.Abstract;
using DataAccessLayer.Data;
using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Concrete.EfCore
{
    public class EfCategoryRepo : ICategoryRepo
    {
        private ApplicationDbContext _context;

        public EfCategoryRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Category> Categories => _context.Categories;
    }
}
