using Blog.Application.UseCases;
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
    public class UseCaseLogsController : ControllerBase
    {
        private UseCaseHandler _handler;

        public UseCaseLogsController(UseCaseHandler handler)
        {
            _handler = handler;
        }
        /// <summary>
        /// Search use case logs
        /// </summary>
        /// <param name="search"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///  GET /api/useCaseLogs
        /// QueryString
        ///  "username": "pera",
        ///  "useCaseName": "create",
        ///  "dateFrom": "2022-06-06",
        ///  "dateTo": "2022-06-10",
        ///  "perPage": 10,
        ///  "page" : 2
        /// </remarks>
        /// <response code="401">Unauthorized.</response>
        /// <response code="500">Unexpected server error.</response>
        [HttpGet]
        public IActionResult Get([FromQuery] UseCaseLogSearch search, [FromServices] IGetUseCaseLogsQuery query)
        {
            return Ok(_handler.HandleQuery(query, search)); 
        }
    }
}
