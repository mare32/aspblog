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
    public class AdminController : ControllerBase
    {
        private UseCaseHandler _handler;
        public AdminController(UseCaseHandler handler)
        {
            _handler = handler;
        }
        [HttpPatch]
        public IActionResult ChangeUserRole([FromBody] ChangeRoleDto dto, [FromServices] IChangeUserRoleCommand command)
        {
            _handler.HandleCommand(command, dto);
            return Ok();
        }
    }
}
