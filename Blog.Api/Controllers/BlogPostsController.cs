using Blog.Api.Core.DTO;
using Blog.Application.UseCases.Commands;
using Blog.Application.UseCases.DTO;
using Blog.Application.UseCases.DTO.Base;
using Blog.Application.UseCases.Queries;
using Blog.Domain;
using Blog.Implementation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Blog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BlogPostsController : ControllerBase
    {
        private UseCaseHandler _handler;
        private IApplicationUser _user;
        public static IEnumerable<string> AllowedExtensions => new List<string> { ".jpg",".png",".jpeg",".gif" };
        public BlogPostsController(UseCaseHandler handler, IApplicationUser user)
        {
            _handler = handler;
            _user = user;
        }
        // GET: api/<BlogPostsController>
        [HttpGet]
        public IActionResult Get([FromQuery] BasePagedSearch search, [FromServices] ISearchBlogPostsQuery query)
        {

            return Ok(_handler.HandleQuery(query,search));
        }

        // GET api/<BlogPostsController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id, [FromServices] IGetOneBlogPostQuery query)
        {
            return Ok(_handler.HandleQuery(query,id));
        }

        // POST api/<BlogPostsController>
        [HttpPost]
        public IActionResult Post([FromForm]CreateBlogPostWithImageDto dto,[FromServices]ICreateBlogPostCommand command)
        {

            // Apstrakovati sve ovo, mozda dodati u helpers
            var imgAlt = "";
            var guid = Guid.NewGuid().ToString();
            var extension = Path.GetExtension(dto.Image.FileName); 
            if(!AllowedExtensions.Contains(extension))
            {
                throw new InvalidOperationException("Los tip slike.");
            }
            if(dto.CategoryIds == null || dto.CategoryIds.Count() < 1 )
            {
                return UnprocessableEntity(new { error = "Izaberite makar jednu kategoriju." });
                //throw new InvalidOperationException("Izaberite makar jednu kategoriju.");
            }
            imgAlt = string.IsNullOrEmpty(dto.ImageAlt) ? "altNotSpecified" : dto.ImageAlt;

            var imeSlike = guid + extension;
            var putanja = Path.Combine("wwwroot", "images", imeSlike);
            
            //
            CreateBlogPostDto noviDto = new CreateBlogPostDto
            {
                Title = dto.Title,
                BlogPostContent = dto.BlogPostContent,
                AuthorId = _user.Id,
                CategoryIds = dto.CategoryIds,
                ImageAlt = imgAlt,
                ImageSrc = putanja
            };
            _handler.HandleCommand(command, noviDto);

            using(var stream = new FileStream(putanja, FileMode.Create))
            { dto.Image.CopyTo(stream); }

            return StatusCode(201);
        }

        // DELETE api/<BlogPostsController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices]IDeleteBlogPostCommand command)
        {
            _handler.HandleCommand(command, id);
            return NoContent();
        }

        [HttpPatch]
        public IActionResult Patch([FromBody] PatchBlogPostDto dto, [FromServices]IPatchBlogPostCommand command)
        {
            _handler.HandleCommand(command, dto);
            return Ok();
        }
    }
}
