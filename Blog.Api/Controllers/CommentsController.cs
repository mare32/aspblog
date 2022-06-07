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

        [HttpPost]
        public IActionResult Post([FromBody] CommentDto dto, [FromServices] ICreateCommentCommand command)
        {
            _handler.HandleCommand(command, dto);
            return StatusCode(201);
        }

        [HttpGet("{id}")]
        public IActionResult GetCommentsByPostId(int id, [FromServices]IShowCommentsQuery query)
        {
            return Ok(_handler.HandleQuery(query,id));
        }
        [HttpDelete("{id}")]
        public IActionResult DeleteComment(int id, [FromServices]IDeleteCommentCommand command)
        {
            _handler.HandleCommand(command, id);
            return NoContent();
        }
    }
}
