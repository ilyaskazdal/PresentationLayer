using DataAccessLayer.Entities;

namespace PresentationLayer.Models
{
    public class MessageViewModel
    {
      
            public int Id { get; set; }
            public string SenderId { get; set; }
            public string ReceiverId { get; set; }
            public string Content { get; set; }
            public DateTime Timestamp { get; set; }

            public User Sender { get; set; }
            public User Receiver { get; set; }
        

    }
}
