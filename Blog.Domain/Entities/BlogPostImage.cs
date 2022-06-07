using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Entities
{
    public class BlogPostImage
    {
        public int PostId { get; set; }
        public int ImageId { get; set; }
        public virtual BlogPost BlogPost { get; set; }
        public virtual Image Image { get; set; }
    }
}
