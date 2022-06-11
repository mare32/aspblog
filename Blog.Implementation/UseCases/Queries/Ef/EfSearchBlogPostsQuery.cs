using Blog.Application.UseCases.DTO;
using Blog.Application.UseCases.DTO.Base;
using Blog.Application.UseCases.Queries;
using Blog.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Implementation.UseCases.Queries.Ef
{
    public class EfSearchBlogPostsQuery : EfUseCase, ISearchBlogPostsQuery
    {
        public EfSearchBlogPostsQuery(BlogContext context) : base(context)
        {
        }

        public int Id => 2006;

        public string Name => "Search BlogPosts";

        public string Description => "Searching blog posts with a keyword using EF";

        public PagedResponse<BlogPostDto> Execute(BasePagedSearch search)
        {
            var query = Context.BlogPosts.Include( x => x.Author).AsQueryable();

            if (!string.IsNullOrEmpty(search.Keyword))
            {
                query = query.Where(x => x.Title.Contains(search.Keyword) || x.Author.Username.Contains(search.Keyword));
            }

            if (search.PerPage == null || search.PerPage < 1)
            {
                search.PerPage = 5;
            }

            if (search.Page == null || search.Page < 1)
            {
                search.Page = 1;
            }

            var toSkip = (search.Page.Value - 1) * search.PerPage.Value;

            var response = new PagedResponse<BlogPostDto>();
            response.TotalCount = query.Count();
            response.Data = query.Skip(toSkip).Take(search.PerPage.Value).Select(x => new BlogPostDto
            {
                Id = x.Id,
                Title = x.Title,
                BlogPostContent = x.BlogPostContent,
                Author = new UserDto
                {
                    Id = x.Author.Id,
                    FirstName = x.Author.FirstName,
                    LastName = x.Author.LastName,
                    Username = x.Author.Username,
                    Email = x.Author.Email
                },
                CoverImageId = x.CoverImage,
                BlogPostImageIds = x.BlogPostImages.Select(y => y.ImageId).ToArray(),
                Categories = x.BlogPostCategories.Select( y => new CategoryDto 
                { 
                    Name = y.Category.Name, 
                    Id = y.Category.Id
                }).ToList(),
                TotalVotes = x.Votes.Count,
                UpVotes = x.Votes.Where(y => y.TypeId == 1).Count(),
                DownVotes = x.Votes.Where(y => y.TypeId == 2).Count(),
                VoteScore = x.Votes.Where(y => y.TypeId == 1).Count() - x.Votes.Where(y => y.TypeId == 2).Count()
            }).ToList();
            response.CurrentPage = search.Page.Value;
            response.ItemsPerPage = search.PerPage.Value;

            return response;
        }
    }
}
