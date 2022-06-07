
using Blog.Application.UseCases.Commands;
using Blog.Application.UseCases.DTO;
using Blog.DataAccess;
using Blog.Domain.Entities;
using Blog.Implementation.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Implementation.UseCases.Commands
{
    public class EfCreateBlogPostCommand : EfUseCase, ICreateBlogPostCommand
    {
        private CreateBlogPostValidator _validator;
        public EfCreateBlogPostCommand(BlogContext context, CreateBlogPostValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 2005;

        public string Name => "Create BlogPost";

        public string Description => "Creating a new blog post using EF";

        public void Execute(CreateBlogPostDto request)
        {
            _validator.ValidateAndThrow(request);
            var coverImg = new Image
            {
                Alt = request.ImageAlt,
                Src = request.ImageSrc
            };
            Context.Images.Add(coverImg);
            Context.SaveChanges();

            var CoverImg = Context.Images.FirstOrDefault(x => x.Src == request.ImageSrc);
            int idCoverImg = CoverImg.Id;
            var post = new BlogPost
            {
                Title = request.Title,
                BlogPostContent = request.BlogPostContent,
                AuthorId = request.AuthorId.Value,
                CoverImage = idCoverImg
            };
            Context.BlogPosts.Add(post);
            Context.SaveChanges();

            var addedPost = Context.BlogPosts.FirstOrDefault(x => x.Title == request.Title && x.AuthorId == request.AuthorId);
            var blogPostImage = new BlogPostImage { ImageId = idCoverImg, PostId = addedPost.Id };
            var blogPostCategories = request.CategoryIds.Select(x => new BlogPostCategory
            {
                PostId = addedPost.Id,
                CategoryId = x
            }).ToList();
            Context.BlogPostImages.Add(blogPostImage);
            Context.BlogPostCategories.AddRange(blogPostCategories);
            Context.SaveChanges();
        }
    }
}
