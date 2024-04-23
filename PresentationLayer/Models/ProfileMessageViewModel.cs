using DataAccessLayer.Entities;

namespace PresentationLayer.Models
{
    public class ProfileMessageViewModel
    {
        public User User { get; set; }
        public string SenderId { get; set; }
        public List<Message> ReceivedMessages { get; set; }
    }
}
