using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Entities
{
    public class BlogPostCategory
    {
        public int PostId { get; set; }
        public int CategoryId { get; set; }

        public virtual Category Category { get; set; }
        public virtual BlogPost BlogPost { get; set; }
    }
}
