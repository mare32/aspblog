using Blog.Application.UseCases.Commands;
using Blog.Domain;
using Blog.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Blog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ImagesController : ControllerBase
    {
        private UseCaseHandler _handler;
        private IApplicationUser _user;
        public ImagesController(UseCaseHandler handler, IApplicationUser user)
        {
            _handler = handler;
            _user = user;
        }

        //[HttpDelete]
        //public IActionResult Delete([FromBody]IEnumerable<int> ids, [FromServices] IDeleteMultipleImagesCommand command)
        //{
        //    _handler.HandleCommand(command, ids);
        //    return NoContent();
        //}
        // DELETE api/<ImagesController>/5



        /// <summary>
        /// Deletes an Image
        /// </summary>
        /// <param name="id"></param>
        /// <param name="command"></param>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///  DELETE /api/images/1
        ///  
        /// </remarks>
        /// <response code="204">No content.</response>
        /// <response code="401">Unauthorized.</response>
        /// <response code="404">Entity not found.</response>
        /// <response code="409">Conflict. Blog post is using it as a cover image.</response>
        /// <response code="500">Unexpected server error.</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        [ProducesResponseType(409)]
        [ProducesResponseType(500)]
        public IActionResult Delete(int id, [FromServices]IDeleteOneImageCommand command)
        {
            _handler.HandleCommand(command, id);
            return NoContent();
        }
    }
}
