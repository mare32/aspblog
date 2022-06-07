using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Entities
{
    public class Image : Entity
    {
        public string Src { get; set; }
        public string Alt { get; set; }
        public virtual ICollection<BlogPostImage> BlogPostImages { get; set; } 
    }
}
