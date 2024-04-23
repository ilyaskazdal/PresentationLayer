using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class Comment
    {   
        public int CommentId { get; set; }
        public string? CommentText { get; set; }
        public DateTime CommentPublishDate { get; set; }

        //1 yorum spesifik bir producta yapılmak zorunda zaten 1 producta 1den fazla yorum yapılabiliyor
        public int ProductPostId { get; set; }
        public ProductPost ProductPost { get; set; } = null!;

        //zaten 1 comment 1 tane usera ait olabilir
        public int UserId { get; set; }
        public virtual User User { get; set; } = null!;
    }
}
