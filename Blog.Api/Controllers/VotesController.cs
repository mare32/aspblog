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
    public class VotesController : ControllerBase
    {
        private UseCaseHandler _handler;
        public VotesController(UseCaseHandler handler)
        {
            _handler = handler;
        }
        /// <summary>
        /// Cast a vote
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///  PUT /api/votes
        ///  {
        ///  "blogPostId": 2,
        ///  "voteType": 1
        ///  }
        ///  {
        ///  "commentId": 2001,
        ///  "voteType": 2
        ///  }
        /// </remarks>
        /// <response code="204">No content.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="409">Conflict.</response>
        /// <response code="500">Unexpected server error.</response>
        [HttpPut]
        [ProducesResponseType(204)]
        [ProducesResponseType(401)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        public IActionResult Put([FromBody] VoteDto dto, [FromServices] ICreateVoteCommand command )
        {
            _handler.HandleCommand(command, dto);
            return NoContent();
        }

        /// <summary>
        /// Deletes a vote
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns>HttpResponseMessage</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///  DELETE api/votes/5
        ///
        /// </remarks>
        /// <response code="204">No Content.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="404">Entity not found.</response>
        /// <response code="500">Unexpected server error.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [ProducesResponseType(500)]
        public IActionResult Delete(int id, [FromServices]IDeleteVoteCommand command)
        {
            _handler.HandleCommand(command, id);
            return NoContent();
        }

        /// <summary>
        /// Search vote from logged-in user
        /// </summary>
        /// <param name="search"></param>
        /// <param name="query"></param>
        /// <returns>Array of votes</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///  GET /api/votes
        ///   QueryString
        ///  "perPage": 5,
        ///  "page": 1,
        ///  "keyword": "p"
        ///
        /// </remarks>
        /// <response code="500">Unexpected server error.</response>
        [HttpGet]
        public IActionResult Get([FromQuery]BasePagedSearch search, [FromServices]ISearchUsersVotesQuery query)
        {
            return Ok(_handler.HandleQuery(query,search));
        }
    }
}
