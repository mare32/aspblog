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
    public class UserUseCasesController : ControllerBase
    {
        private UseCaseHandler _handler;

        public UserUseCasesController(UseCaseHandler handler)
        {
            _handler = handler;
        }

        [HttpPut]
        public IActionResult Put([FromBody] UpdateUserUseCasesDto dto, [FromServices]IUpdateUserUseCasesCommand command)
        {
            _handler.HandleCommand(command, dto);
            return NoContent();
        }
    }
}
