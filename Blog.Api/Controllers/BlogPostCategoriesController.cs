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

        /// <summary>
        /// Update blog post categories
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///  PUT /api/blogpostcategories
        ///  {
        ///  "blogPostId": "2",
        ///  "categoryIds": [2,2017,2007,2012]
        ///  }
        /// </remarks>
        /// <response code="204">No content.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="500">Unexpected server error.</response>
        [HttpPut]
        [ProducesResponseType(204)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public IActionResult Put([FromBody]UpdateBlogPostCategoriesDto dto, [FromServices]IUpdateBlogPostCategoriesCommand command)
        {
            _handler.HandleCommand(command, dto);
            return NoContent();
        }
    }
}
