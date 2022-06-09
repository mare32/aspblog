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
    public class UseCasesController : ControllerBase
    {
        private UseCaseHandler _handler;

        public UseCasesController(UseCaseHandler handler)
        {
            _handler = handler;
        }

        /// <summary>
        /// Search use cases
        /// </summary>
        /// <param name="search"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///  GET /api/usecases
        ///  QueryString
        ///  "keyword": "create",
        ///  "perPage": 10,
        ///  "page" : 2
        /// </remarks>
        /// <response code="401">Unauthorized.</response>
        /// <response code="500">Unexpected server error.</response>
        [HttpGet]
        public IActionResult Get([FromQuery]BasePagedSearch search, [FromServices]ISearchUseCasesQuery query)
        {
            return Ok(_handler.HandleQuery(query, search));
        }
    }
}
