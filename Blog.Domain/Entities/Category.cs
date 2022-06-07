using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Entities
{
    public class Category : Entity
    {
        public string Name { get; set; }

        public virtual ICollection<BlogPostCategory> BlogPostCategories { get; set; }
    }
}
