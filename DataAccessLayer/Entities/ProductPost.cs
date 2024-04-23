using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class ProductPost
    {
        public int ProductPostId { get; set; }
        public string ProductPostName { get; set;}
        public string ProductPostDesccription { get; set;}
        public DateTime DateTime { get; set; }
        public bool StillOnSale { get; set;}

        //1 post 1 usera
        public int UserId { get; set; }
        public User User { get; set; } = null!;

        //1 product 1den fazla categoride bulunabilir (ahşap masa) ev kategorisi ve iş kategorisi gibi veya (kalem) iş kategorisi ve okul kategorisi gibiS
        public List<Category> Categories { get; set; } = new List<Category>();

        public List<Comment> Comments { get; set; } = new List<Comment>();
    }
}
