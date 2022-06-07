using Blog.Application.UseCases.Commands;
using Blog.Application.UseCases.DTO;
using Blog.DataAccess;
using Blog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Implementation.UseCases.Commands
{
    public class EfUpdateBlogPostCategoriesCommand : EfUseCase, IUpdateBlogPostCategoriesCommand
    {
        // validator
        public EfUpdateBlogPostCategoriesCommand(BlogContext context) : base(context)
        {
        }

        public int Id => 2019;

        public string Name => "Update BlogPost Categories";

        public string Description => "Update BlogPost Categories using EF";

        public void Execute(UpdateBlogPostCategoriesDto request)
        {
            // validator

            var blogPostCategories = Context.BlogPostCategories.Where(x => x.PostId == request.BlogPostId);
            Context.BlogPostCategories.RemoveRange(blogPostCategories);

            var blogPostCategoriesToAdd = request.CategoryIds.Select(x => new BlogPostCategory
            {
                CategoryId = x,
                PostId = request.BlogPostId
            });

            Context.BlogPostCategories.AddRange(blogPostCategoriesToAdd);
            Context.SaveChanges();
        }
    }
}
