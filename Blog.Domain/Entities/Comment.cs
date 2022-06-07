using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Entities
{
    public class Comment : Entity
    {
        public int PostId { get; set; }
        public int UserId { get; set; }
        public string CommentText { get; set; }
        public DateTime CommentedAt { get; set; }
        public DateTime? DeletedAt { get; set; }
        public int? ParentId { get; set; }

        public virtual User User { get; set; }
        public virtual BlogPost BlogPost { get; set; }
        public virtual Comment ParentComment { get; set; }
        public virtual ICollection<Vote> Votes { get; set; }
    }
}
