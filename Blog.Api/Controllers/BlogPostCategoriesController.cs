using Blog.Application.UseCases.Commands;
using Blog.Application.UseCases.DTO;
using Blog.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BlogPostCategoriesController : ControllerBase
    {
        private UseCaseHandler _handler;
        public BlogPostCategoriesController(UseCaseHandler handler)
        {
            _handler = handler;
        }
        [HttpPut]
        public IActionResult Put([FromBody]UpdateBlogPostCategoriesDto dto, [FromServices]IUpdateBlogPostCategoriesCommand command)
        {
            _handler.HandleCommand(command, dto);
            return NoContent();
        }
    }
}
