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
    public class VotesController : ControllerBase
    {
        private UseCaseHandler _handler;
        public VotesController(UseCaseHandler handler)
        {
            _handler = handler;
        }

        [HttpPut]
        public IActionResult Put([FromBody] VoteDto dto, [FromServices] ICreateVoteCommand command )
        {
            _handler.HandleCommand(command, dto);
            return NoContent();
        }
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices]IDeleteVoteCommand command)
        {
            _handler.HandleCommand(command, id);
            return NoContent();
        }
    }
}
