using Blog.DataAccess;
using Blog.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Blog.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InitialDataController : ControllerBase
    {

        /// <summary>
        /// Posted the initial data when I started this project(Not for use currently)
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// Sample request:
        ///
        ///  POST api/initialData
        ///
        /// </remarks>
        /// <response code="401">Unauthorized.</response>
        /// <response code="500">Unexpected server error.</response>
        [HttpPost]
        public IActionResult Post()
        {
            var context = new BlogContext();
            // Provera da li data ulazi po prvi put

            if(context.Users.Any())
            {
                return StatusCode(409);
            }


            var roles = new List<Role>
            {
                new Role{ Name = "Admin"},
                new Role{ Name = "Normal"}
            };

            var users = new List<User>
            {
                new User
                {
                    Username = "pera",
                    Email = "pera@gmail.com",
                    Password = "lozinka",
                    FirstName = "Petar",
                    LastName = "Peric"
                },
                new User
                {
                    Username = "mika",
                    Email = "mika@gmail.com",
                    Password = "lozinka",
                    FirstName = "Mitar",
                    LastName = "Mikic",
                    Role = roles.ElementAt(1)
                },
                new User
                {
                    Username = "admin",
                    Email = "admin@gmail.com",
                    Password = "lozinka123",
                    FirstName = "Tika",
                    LastName = "Tikic",
                    RoleId = roles.ElementAt(0).Id,
                    Role = roles.ElementAt(0)
                }
            };

            var images = new List<Image>
            {
                new Image{ Src = @"wwwroot\images\first.jpg", Alt = "Sea Landscape"},
                new Image{ Src = @"wwwroot\images\second.jpg", Alt = "Mountain Landscape"},
            };

            var categories = new List<Category>
            {
                new Category { Name = "Travel"},
                new Category { Name = "Food"},
                new Category { Name = "Health"},
                new Category { Name = "Politics"}
            };

            var votetypes = new List<VoteType>
            {
                new VoteType{ Type = "Upvote"},
                new VoteType{ Type = "Downvote"}
            };

            var usecases = new List<UseCase>
            {
                new UseCase{ Name = "New BlogPost", Description = "Create a new blogpost"},
                new UseCase{ Name = "Delete user", Description = "Delete an existing user"}
            };

            var userusecases = new List<UserUseCase>
            {
                new UserUseCase { UseCase = usecases.First(), User = users.First()},
                new UserUseCase { UseCase = usecases.ElementAt(0), User = users.ElementAt(1)},
                new UserUseCase { UseCase = usecases.ElementAt(0), User = users.ElementAt(2)},
                new UserUseCase { UseCase = usecases.ElementAt(1), User = users.ElementAt(2)},
            };

            var auditlogs = new List<AuditLog>
            {
                new AuditLog { IsAuthorized = true , UseCaseName = "New BlogPost", UserId = 1, Username = "pera", Data="User pera created a new blogpost [test]" },
            };

            var blogposts = new List<BlogPost>
            {
                new BlogPost
                {
                    Title = "Climbing on Himalayas",
                    CoverImage = 1,
                    BlogPostContent = "Climbing up theres was a real challenge but somehow we actualy made it in one piece, gettin down was even harder.",
                    Author = users.First()
                }
            };

            var blogpostcategories = new List<BlogPostCategory>
            {
                new BlogPostCategory{ BlogPost = blogposts.First(), Category = categories.First() },
                new BlogPostCategory{ BlogPost = blogposts.First(), Category = categories.ElementAt(2) },
            };

            var blogpostimages = new List<BlogPostImage>
            {
                new BlogPostImage {BlogPost = blogposts.First(), Image = images.First()},
                new BlogPostImage {BlogPost = blogposts.First(), Image = images.ElementAt(1)},
            };
            var comments = new List<Comment>
            {
                new Comment { BlogPost = blogposts.First(),CommentText = "Woah that is so crazy!", User = users.ElementAt(1)}
            };
            var subcomment = new Comment { BlogPost = blogposts.First(), CommentText = "Thank you, we barely survived haha", User = users.ElementAt(0), ParentComment = comments.First() };
            comments.Add(subcomment);

            var votes = new List<Vote>
            {
                new Vote{ VoteType = votetypes.First(), User = users.ElementAt(1),BlogPost = blogposts.First()},
                new Vote{ VoteType = votetypes.ElementAt(1), User = users.ElementAt(0),Comment = comments.ElementAt(0)}
            };

            context.Roles.AddRange(roles);
            context.Users.AddRange(users);
            context.Images.AddRange(images);
            context.Categories.AddRange(categories);
            context.VoteTypes.AddRange(votetypes);
            context.UseCases.AddRange(usecases);
            context.UserUseCases.AddRange(userusecases);
            context.AuditLogs.AddRange(auditlogs);
            context.BlogPosts.AddRange(blogposts);
            context.BlogPostCategories.AddRange(blogpostcategories);
            context.BlogPostImages.AddRange(blogpostimages);
            context.Comments.AddRange(comments);
            context.Votes.AddRange(votes);

            context.SaveChanges();
            return StatusCode(201);
        }

    }
}
