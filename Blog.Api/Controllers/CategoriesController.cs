using Blog.Application.UseCases.Commands;
using Blog.Application.UseCases.DTO;
using Blog.Application.UseCases.DTO.Base;
using Blog.Application.UseCases.Queries;
using Blog.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Blog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoriesController : ControllerBase
    {
        private UseCaseHandler _handler;
        public CategoriesController(UseCaseHandler handler)
        {
            _handler = handler;
        }
        /// <summary>
        /// Search categories.
        /// </summary>
        /// <param name="search"></param>
        /// <param name="query"></param>
        /// <returns>Array of categories</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///  GET /api/Categories
        ///   QueryString
        ///  "perPage": 5,
        ///  "page": 1,
        ///  "keyword": "t"
        ///
        /// </remarks>
        /// <response code="500">Unexpected server error.</response>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get([FromQuery] BasePagedSearch search, [FromServices] IGetCategoriesQuery query)
        {
            return Ok(_handler.HandleQuery(query,search));
        }

        /// <summary>
        /// Shows a category.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="query"></param>
        /// <returns>Category</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///  GET api/categories/5
        ///
        /// </remarks>
        /// <response code="404">Entity not found.</response>
        /// <response code="500">Unexpected server error.</response>
        [HttpGet("{id}")]
        public IActionResult Get(int id, [FromServices]IGetOneCategoryQuery query)
        {
            return Ok(_handler.HandleQuery(query, id));
        }

        /// <summary>
        /// Creates new category.
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="command"></param>
        /// <returns>HttpResponseMessage</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///     POST /api/Categories
        ///     {
        ///        "name": "Nova Kategorija"
        ///     }
        ///
        /// </remarks>
        /// <response code="201">Created.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="422">Validation failure.</response>
        /// <response code="500">Unexpected server error.</response>
        [HttpPost]
        [ProducesResponseType(201)]
        [ProducesResponseType(401)]
        [ProducesResponseType(422)]
        [ProducesResponseType(500)]
        public IActionResult Post([FromBody] CategoryDto dto, [FromServices] ICreateCategoryCommand command)
        {
            _handler.HandleCommand(command, dto);
            return StatusCode(201);
        }
        /// <summary>
        /// Deletes a category.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns>HttpResponseMessage</returns>
        /// <remarks>
        /// Sample request:
        ///
        ///  DELETE api/categories/5
        ///
        /// </remarks>
        /// <response code="204">No Content.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="404">Entity not found.</response>
        /// <response code="409">Conflict. Category is used by blog posts.</response>
        /// <response code="500">Unexpected server error.</response>

        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        public IActionResult Delete(int id, [FromServices] IDeleteCategoryCommand command)
        {
            _handler.HandleCommand(command, id);
            return NoContent();
        }
    }
}
