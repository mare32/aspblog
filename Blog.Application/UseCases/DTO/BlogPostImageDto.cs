using Blog.Application.UseCases.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.UseCases.DTO
{
    public class BlogPostImageDto : BaseDto
    {
        public int BlogPostId { get; set; }
        public string BlogPostTitle { get; set; }
        public string AuthorUsername { get; set; }
        public string ImageSrc { get; set; }
        public string ImageAlt { get; set; }
    }
}
