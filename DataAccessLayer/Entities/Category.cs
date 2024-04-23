using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    // Categoriler ev iş okul elektronik yiyecek dekorasyon gibi gibi bu yüzden 1 ürün 1den fazla kategorde olabilir
    public class Category
    {
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }

        //1 kategorinin 1den fazla produğı olıcak tabiki
        public List<ProductPost> ProductPosts { get; set; } = new List<ProductPost>();

    }
}
