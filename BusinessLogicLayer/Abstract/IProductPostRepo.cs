using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Abstract
{
    public interface IProductPostRepo
    {
        IQueryable<ProductPost> ProductPosts { get; }

        void CreateProductPost(ProductPost productPost);

        void EditProductPost(ProductPost productPost);
        void DeleteProduct(int id);
    }
}
