using Blog.Application.Exceptions;
using Blog.Application.UseCases.DTO;
using Blog.Application.UseCases.Queries;
using Blog.DataAccess;
using Blog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Implementation.UseCases.Queries.Ef
{
    public class EfGetOneBlogPostQuery : EfUseCase, IGetOneBlogPostQuery
    {
        public EfGetOneBlogPostQuery(BlogContext context) : base(context)
        {
        }

        public int Id => 2007;

        public string Name => "View Blog post";

        public string Description => "See blog post by Id using EF";

        public BlogPostDto Execute(int id)
        {
            var blogPost = Context.BlogPosts.FirstOrDefault(x => x.Id == id);
            if (blogPost == null)
            {
                throw new EntityNotFoundException(nameof(BlogPost), id);
            }
            return new BlogPostDto
            {
                Id = blogPost.Id,
                Title = blogPost.Title,
                BlogPostContent = blogPost.BlogPostContent,
                Author = new UserDto
                {
                    Id = blogPost.Author.Id,
                    FirstName = blogPost.Author.FirstName,
                    LastName = blogPost.Author.LastName,
                    Username = blogPost.Author.Username,
                    Email = blogPost.Author.Email
                },
                CoverImageId = blogPost.CoverImage,
                BlogPostImageIds = blogPost.BlogPostImages.Select(y => y.ImageId).ToArray(),
                Categories = blogPost.BlogPostCategories.Select(y => new CategoryDto
                {
                    Name = y.Category.Name,
                    Id = y.Category.Id
                }).ToList(),
                TotalVotes = blogPost.Votes.Count,
                UpVotes = blogPost.Votes.Where(y => y.TypeId == 1).Count(),
                DownVotes = blogPost.Votes.Where(y => y.TypeId == 2).Count(),
                VoteScore = blogPost.Votes.Where(y => y.TypeId == 1).Count() - blogPost.Votes.Where(y => y.TypeId == 2).Count()
            };
        }
    }
}
