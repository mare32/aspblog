using Blog.Api.Core.DTO;
using Blog.Api.Core.ImageHelpers;
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
    public class BlogPostImagesController : ControllerBase
    {
        private UseCaseHandler _handler;
        private IApplicationUser _user;
        public BlogPostImagesController(UseCaseHandler handler, IApplicationUser user)
        {
            _handler = handler;
            _user = user;
        }

        // GET: api/<BlogPostImagesController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<BlogPostImagesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<BlogPostImagesController>
        [HttpPost]
        public IActionResult Post([FromForm] ImageDto dto, [FromServices] IAddImageToBlogPostCommand command)
        {
            _handler.HandleCommand(command, dto);
            return StatusCode(201);
        }

        // PUT api/<BlogPostImagesController>
        [HttpPut]
        public IActionResult Put()
        {
            // 5 je postId a kroz body dolaze slike i altovi a nmg tri parametrra da imam
            return Ok();
        }

        // DELETE api/<BlogPostImagesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
