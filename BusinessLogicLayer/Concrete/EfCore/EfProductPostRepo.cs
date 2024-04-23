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
    public class EfProductPostRepo : IProductPostRepo
    {
        private ApplicationDbContext _context;

        public EfProductPostRepo(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<ProductPost> ProductPosts => _context.ProductPosts;

        public void CreateProductPost(ProductPost productPost)
        {
            _context.ProductPosts.Add(productPost);
            _context.SaveChanges();
        }

        public void DeleteProduct(int id)
        {
            var product = _context.ProductPosts.Find(id);
            if (product != null)
            {
                _context.ProductPosts.Remove(product);
                _context.SaveChanges();
            }
        }

        public void EditProductPost(ProductPost productPost)
        {
            var entity = _context.ProductPosts.FirstOrDefault(i=>i.ProductPostId == productPost.ProductPostId);
            if (entity != null)
            {
                entity.ProductPostName = productPost.ProductPostName;
                entity.ProductPostDesccription = productPost.ProductPostDesccription;
                entity.StillOnSale = productPost.StillOnSale;
                _context.SaveChanges();
            }
        }
    }
}
