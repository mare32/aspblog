using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.UseCases.DTO
{
    public class UpdateBlogPostCategoriesDto
    {
        public int BlogPostId { get; set; }
        public IEnumerable<int> CategoryIds { get; set; }
    }
}
