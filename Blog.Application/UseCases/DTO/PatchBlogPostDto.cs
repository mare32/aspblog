using Blog.Application.UseCases.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.UseCases.DTO
{
    public class PatchBlogPostDto : BaseDto
    {
        public string Title { get; set; }
        public string BlogPostContent { get; set; }
        public int? CoverImgId { get; set; }
    }
}
