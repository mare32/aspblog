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

        // GET: api/<UsersController>
        [HttpGet]
        public IActionResult Get([FromQuery] BasePagedSearch search, [FromServices] ISearchUsersQuery query)
        {
            return Ok(_handler.HandleQuery(query, search));
        }

        // GET api/users/1
        [HttpGet("{id}")]
        public IActionResult Get(int id, [FromServices] IGetOneUserQuery query)
        {
            return Ok(_handler.HandleQuery(query, id));
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices]IDeleteUserCommand command)
        {
            //var blogPostController = new BlogPostsController(_handler, _user);
            //var blogPosts = _context.BlogPosts.Where(x => x.AuthorId == id);
            //if(blogPosts != null)
            //{
            //    foreach(var post in blogPosts)
            //    {
            //        blogPostController.Delete(post.Id, new EfDeleteBlogPostCommand(_context,_user));
            //    }
            //}
            _handler.HandleCommand(command, id);
            return NoContent();
        }
        
        [HttpPatch]
        public IActionResult UpdateUserProfile([FromBody]UpdateUserProfileDto dto, [FromServices]IUpdateUserProfileCommand command)
        {
            _handler.HandleCommand(command, dto);
            return Ok();
        }
    }
}
