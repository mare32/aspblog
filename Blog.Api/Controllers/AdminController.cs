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

        /// <summary>
        /// Change users role
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///  PATCH /api/admin
        ///  {
        ///  "roleId": 1,
        ///  "userId": 3
        ///  }
        /// </remarks>
        /// <response code="401">Unauthorized.</response>
        /// <response code="404">Entity not found.</response>
        /// <response code="409">Conflict. Either user already has that role or you are trying to change your own role.</response>
        /// <response code="500">Unexpected server error.</response>
        [HttpPatch]
        public IActionResult ChangeUserRole([FromBody] ChangeRoleDto dto, [FromServices] IChangeUserRoleCommand command)
        {
            _handler.HandleCommand(command, dto);
            return Ok();
        }
    }
}
