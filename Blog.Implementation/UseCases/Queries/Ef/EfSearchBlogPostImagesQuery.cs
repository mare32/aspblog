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
    public class EfSearchBlogPostImagesQuery : EfUseCase, ISearchBlogPostImagesQuery
    {
        public EfSearchBlogPostImagesQuery(BlogContext context) : base(context)
        {
        }

        public int Id =>2029;

        public string Name => "Search blog post images";

        public string Description => "Search BlogPostImages by keyword using EF";

        public PagedResponse<BlogPostImageDto> Execute(BasePagedSearch search)
        {
            var query = Context.BlogPostImages.Include( x => x.BlogPost).ThenInclude( x => x.Author).Include( x => x.Image).AsQueryable();
            if (!string.IsNullOrEmpty(search.Keyword))
            {
                query = query.Where(x => x.BlogPost.Author.Username.Contains(search.Keyword) || x.BlogPost.Title.Contains(search.Keyword));
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

            var response = new PagedResponse<BlogPostImageDto>();
            response.TotalCount = query.Count();
            response.Data = query.Skip(toSkip).Take(search.PerPage.Value).Select(x => new BlogPostImageDto
            {
                Id = x.ImageId,
                BlogPostId = x.BlogPost.Id,
                BlogPostTitle = x.BlogPost.Title,
                AuthorUsername = x.BlogPost.Author.Username,
                ImageSrc = x.Image.Src,
                ImageAlt= x.Image.Alt,
            }).ToList();
            response.CurrentPage = search.Page.Value;
            response.ItemsPerPage = search.PerPage.Value;

            return response;
        }
    }
}
