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
        [HttpDelete]
        public IActionResult Delete([FromBody]IEnumerable<int> ids, [FromServices] IDeleteMultipleImagesCommand command)
        {
            _handler.HandleCommand(command, ids);
            return NoContent();
        }
        // DELETE api/<ImagesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices]IDeleteOneImageCommand command)
        {
            _handler.HandleCommand(command, id);
            return NoContent();
        }
    }
}
