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


        /// <summary>
        /// Update users use cases
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///  PUT /api/userUseCases
        ///  {
        ///  "userId": "2",
        ///  "useCaseIds": [2,2017,2007,2012]
        ///  }
        /// </remarks>
        /// <response code="204">No content.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="500">Unexpected server error.</response>
        [HttpPut]
        [ProducesResponseType(204)]
        [ProducesResponseType(401)]
        [ProducesResponseType(500)]
        public IActionResult Put([FromBody] UpdateUserUseCasesDto dto, [FromServices]IUpdateUserUseCasesCommand command)
        {
            _handler.HandleCommand(command, dto);
            return NoContent();
        }
    }
}
