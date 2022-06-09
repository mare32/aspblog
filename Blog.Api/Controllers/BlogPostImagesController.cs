using Blog.Api.Core.DTO;
using Blog.Api.Core.ImageHelpers;
using Blog.Domain;
using Blog.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Blog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BlogPostImagesController : ControllerBase
    {
        private UseCaseHandler _handler;
        private IApplicationUser _user;
        public BlogPostImagesController(UseCaseHandler handler, IApplicationUser user)
        {
            _handler = handler;
            _user = user;
        }

        /// <summary>
        /// Adds an image to a blog post.
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="command"></param>
        /// <returns>HttpResponseMessage</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/Categories
        ///     {
        ///        "image": "fileUpload",
        ///        "blogPostId": 2
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Created.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="500">Unexpected server error.</response>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public IActionResult Post([FromForm] ImageDto dto, [FromServices] IAddImageToBlogPostCommand command)
        {
            _handler.HandleCommand(command, dto);
            return StatusCode(201);
        }
    }
}
