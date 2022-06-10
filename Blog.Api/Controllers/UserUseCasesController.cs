using Blog.Application.UseCases.Commands;
using Blog.Application.UseCases.DTO;
using Blog.Application.UseCases.DTO.Base;
using Blog.Application.UseCases.Queries;
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

        /// <summary>
        /// Search User use cases.
        /// </summary>
        /// <param name="search"></param>
        /// <param name="query"></param>
        /// <returns>UserUseCases</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///  GET /api/userusecases
        ///   QueryString
        ///  "perPage": 5,
        ///  "page": 1,
        ///  "keyword": "p"
        ///
        /// </remarks>
        /// <response code="500">Unexpected server error.</response>
        [HttpGet]
        public IActionResult Get([FromQuery]BasePagedSearch search, [FromServices]ISearchUserUseCasesQuery query)
        {
            return Ok(_handler.HandleQuery(query, search));
        }

        /// <summary>
        /// Add UserUseCase
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="command"></param>
        /// <returns>HttpResponseMessage</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/userusecases
        ///     {
        ///        "userId":2003,
        ///        "useCaseId":2005
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Successfull creation.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="409">Conflict.</response>
        /// <response code="422">Validation failure.</response>
        /// <response code="500">Unexpected server error.</response>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(401)]
        [ProducesResponseType(409)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public IActionResult Post([FromBody]UpdateUserUseCaseDto dto, [FromServices]IAddUserUseCaseCommand command)
        {
            _handler.HandleCommand(command, dto);
            return StatusCode(201);
        }
        /// <summary>
        /// Deletes a UserUseCase.
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="command"></param>
        /// <returns>HttpResponseMessage</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///  DELETE api/userusecases
        ///  {
        ///     "userId":2003,
        ///     "useCaseId":2005
        ///  }
        ///
        /// </remarks>
        /// <response code="204">No Content.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="404">Entity not found.</response>
        /// <response code="500">Unexpected server error.</response>

        [HttpDelete]
        [ProducesResponseType(204)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult Delete([FromBody]UpdateUserUseCaseDto dto, [FromServices]IRemoveUserUseCaseCommand command)
        {
            _handler.HandleCommand(command, dto);
            return NoContent();
        }
    }
}
