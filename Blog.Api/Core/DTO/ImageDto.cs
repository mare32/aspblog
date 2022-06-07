using Blog.Application.UseCases.DTO.Base;
using Microsoft.AspNetCore.Http;

namespace Blog.Api.Core.DTO
{
    public class ImageDto : BaseDto
    {
        public IFormFile Image { get; set; }
        public int BlogPostId { get; set; }
    }
}
