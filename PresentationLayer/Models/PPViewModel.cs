using DataAccessLayer.Entities;

namespace PresentationLayer.Models
{
    public class PPViewModel
    {
        public List<ProductPost> ProductPosts { get; set; } = new();
        public int ProductPostId { get; set; }


    }
}
