using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Entities
{

    public class VoteType : Entity
    {
        public string Type { get; set; } // Like of Dislike How to make it be an enum

        public virtual ICollection<Vote> Votes { get; set; }
    }
}
