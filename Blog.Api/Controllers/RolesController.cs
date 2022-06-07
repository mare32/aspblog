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
    public class RolesController : ControllerBase
    {
        private UseCaseHandler _handler;
        public RolesController(UseCaseHandler handler)
        {
            _handler = handler;
        }
        [HttpGet]
        public IActionResult Get([FromQuery] BaseSearch search,[FromServices]ISearchRolesQuery query)
        {
            return Ok(_handler.HandleQuery(query, search));
        }
    }
}
