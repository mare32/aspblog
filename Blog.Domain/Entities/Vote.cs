using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Entities
{
    public class Vote : Entity
    {
        public int UserId { get; set; }
        public int? PostId { get; set; }
        public int? CommentId { get; set; }
        public int TypeId { get; set; }
        public DateTime VotedAt { get; set; }

        public virtual VoteType VoteType { get; set; }
        public virtual User User { get; set; }
        public virtual BlogPost BlogPost { get; set; }
        public virtual Comment Comment { get; set; }
    }
}
