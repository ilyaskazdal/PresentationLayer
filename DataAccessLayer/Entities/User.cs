using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class User
    {
        public int UserId { get; set; }
        public string UserNickName { get; set; }
        public string UserName { get; set; }
        public string UserSurname { get; set; }
        public string UserDescription { get; set; }
        public string UserEmail { get; set; }
        public string Password { get; set; }
        public bool CheckedByAdmin { get; set; }
        public DateTime UserCreatedDate { get; set; }

        // 1den fazla product publishleyebilir list
        public List<ProductPost> ProductPosts { get; set; } = new List<ProductPost>();
        public virtual ICollection<Comment> Comments { get; set; }

    }
}
