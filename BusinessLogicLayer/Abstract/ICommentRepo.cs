using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Abstract
{
    public interface ICommentRepo
    {
        IQueryable<Comment> Comments { get; }
        void CreateComment(Comment comment);
    }
}
