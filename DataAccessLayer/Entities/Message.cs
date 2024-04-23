using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class Message
    {
        public int MessageId { get; set; }
        public int MessageSenderId { get; set; }
        public int MessageReceiverId { get; set; }
        public string MessageContent { get; set; }
        public DateTime MessageDateTime { get; set; }

        public User MessageSender { get; set; }
        public User MessageReceiver { get; set; }
    }
}
