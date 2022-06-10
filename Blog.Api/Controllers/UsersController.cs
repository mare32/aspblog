using Blog.Application.UseCases.Commands;
using Blog.Application.UseCases.DTO;
using Blog.Application.UseCases.DTO.Base;
using Blog.Application.UseCases.Queries;
using Blog.DataAccess;
using Blog.Domain;
using Blog.Implementation;
using Blog.Implementation.UseCases.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Blog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private UseCaseHandler _handler;
        IApplicationUser _user;
        BlogContext _context;
        public UsersController(UseCaseHandler handler, IApplicationUser user, BlogContext context)
        {
            _context = context;
            _user = user;
            _handler = handler;
        }

        /// <summary>
        /// Search users.
        /// </summary>
        /// <param name="search"></param>
        /// <param name="query"></param>
        /// <returns>Array of categories</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///  GET /api/users
        ///   QueryString
        ///  "perPage": 5,
        ///  "page": 1,
        ///  "keyword": "a"
        ///
        /// </remarks>
        /// <response code="401">Unauthorized.</response>
        /// <response code="500">Unexpected server error.</response>
        [HttpGet]
        public IActionResult Get([FromQuery] BasePagedSearch search, [FromServices] ISearchUsersQuery query)
        {
            return Ok(_handler.HandleQuery(query, search));
        }

        /// <summary>
        /// Shows a user.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="query"></param>
        /// <returns>User</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///  GET api/users/5
        ///
        /// </remarks>
        /// <response code="401">Unauthorized.</response>
        /// <response code="404">Entity not found.</response>
        /// <response code="500">Unexpected server error.</response>
        [HttpGet("{id}")]
        public IActionResult Get(int id, [FromServices] IGetOneUserQuery query)
        {
            return Ok(_handler.HandleQuery(query, id));
        }

        /// <summary>
        /// Deletes a user
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns>HttpResponseMessage</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///  DELETE api/users/5
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
        public IActionResult Delete(int id, [FromServices]IDeleteUserCommand command)
        {
            _handler.HandleCommand(command, id);
            return NoContent();
        }

        /// <summary>
        /// Update user
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///  PATCH /api/users
        ///  {
        ///  "id":2
        ///  "firstname": "null",
        ///  "lastname": "NewLastName",
        ///  "username": "NewUserName",
        ///  "email" : "null",
        ///  "password" : "null"
        ///  }
        /// </remarks>
        /// <response code="401">Unauthorized.</response>
        /// <response code="404">Entity not found.</response>
        /// <response code="500">Unexpected server error.</response>
        [HttpPatch]
        public IActionResult UpdateUserProfile([FromBody]UpdateUserProfileDto dto, [FromServices]IUpdateUserProfileCommand command)
        {
            _handler.HandleCommand(command, dto);
            return Ok();
        }
    }
}
