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
        /// <summary>
        /// Registers a user.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="command"></param>
        /// <returns>HttpResponseMessage</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/register
        ///     {
        ///         "firstname": "Pera",
        ///         "lastname": "Peric",
        ///         "username": "perica",
        ///         "email" : "perica@gmail.com",
        ///         "password" : "321sifra"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Created.</response>
        /// <response code="422">Validation failure.</response>
        /// <response code="500">Unexpected server error.</response>
        [HttpPost]
        [AllowAnonymous]
        [ProducesResponseType(201)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public IActionResult Post([FromBody] RegisterDto request,[FromServices]IRegisterUserCommand command)
        {
            _handler.HandleCommand(command, request);
            return StatusCode(201);
        }
    }
}
