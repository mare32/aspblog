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
        [HttpGet]
        public IActionResult Get([FromQuery] UseCaseLogSearch search, [FromServices] IGetUseCaseLogsQuery query)
        {
            return Ok(_handler.HandleQuery(query, search)); 
        }
    }
}
