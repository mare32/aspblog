using Blog.Application.UseCases.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.UseCases.DTO
{
    public class CreateBlogPostDto
    {
        public string Title { get; set; }
        public string BlogPostContent { get; set; }
        public int? AuthorId { get; set; }
        public IEnumerable<int> CategoryIds { get; set; }
        public string ImageSrc { get; set; }
        public string ImageAlt { get; set; }

    }
}
