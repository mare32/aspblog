using Blog.Application.UseCases.DTO;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Blog.Api.Core.DTO
{
    public class CreateBlogPostWithImageDto : CreateBlogPostDto
    {
        public IFormFile Image { get; set; }
    }
}
