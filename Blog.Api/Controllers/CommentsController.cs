using Blog.Application.UseCases.Commands;
using Blog.Application.UseCases.DTO;
using Blog.Application.UseCases.Queries;
using Blog.Domain;
using Blog.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Blog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CommentsController : ControllerBase
    {
        private UseCaseHandler _handler;
        private IApplicationUser _user;
        public CommentsController(IApplicationUser user, UseCaseHandler handler)
        {
            _handler = handler;
            _user = user;
        }

        /// <summary>
        /// Creates new comment.
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="command"></param>
        /// <returns>HttpResponseMessage</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/comments
        ///     {
        ///        "commentText": "This is a comment example",
        ///        "blogPostId" : 2,
        ///        "parentId": null
        ///     }
        ///     POST /api/comments
        ///     {
        ///        "commentText": "This is a reply example",
        ///        "blogPostId" : 2,
        ///        "parentId": 2005
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Created.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="422">Validation failure.</response>
        /// <response code="500">Unexpected server error.</response>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(401)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public IActionResult Post([FromBody] CommentDto dto, [FromServices] ICreateCommentCommand command)
        {
            _handler.HandleCommand(command, dto);
            return StatusCode(201);
        }
        /// <summary>
        /// Shows all comments by BlogPost id
        /// </summary>
        /// <param name="id"></param>
        /// <param name="query"></param>
        /// <returns>Comments</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///  GET api/comments/5
        ///
        /// </remarks>
        /// <response code="404">Entity not found.</response>
        /// <response code="500">Unexpected server error.</response>
        [HttpGet("{id}")]
        [AllowAnonymous]
        public IActionResult GetCommentsByPostId(int id, [FromServices]IShowCommentsQuery query)
        {
            return Ok(_handler.HandleQuery(query,id));
        }
        /// <summary>
        /// Deletes a comment.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns>HttpResponseMessage</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///  DELETE api/comments/5
        ///
        /// </remarks>
        /// <response code="401">Unauthorized.</response>
        /// <response code="204">No Content.</response>
        /// <response code="404">Entity not found.</response>
        /// <response code="500">Unexpected server error.</response>

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        public IActionResult DeleteComment(int id, [FromServices]IDeleteCommentCommand command)
        {
            _handler.HandleCommand(command, id);
            return NoContent();
        }
    }
}
