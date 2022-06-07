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
    public class RegisterController : ControllerBase
    {
        private UseCaseHandler _handler;
        public RegisterController(UseCaseHandler handler)
        {
            _handler = handler;
        }
        [HttpPost]
        [AllowAnonymous]
        public IActionResult Post([FromBody] RegisterDto request,[FromServices]IRegisterUserCommand command)
        {
            _handler.HandleCommand(command, request);
            return StatusCode(201);
        }
    }
}
