using Blog.Api.Core.DTO;
using Blog.Application.Exceptions;
using Blog.DataAccess;
using Blog.Domain;
using Blog.Domain.Entities;
using Blog.Implementation.UseCases;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Blog.Api.Core.ImageHelpers
{
    public class EfAddImageToBlogPostCommand : EfUseCase, IAddImageToBlogPostCommand
    {
        private IEnumerable<string> errors = new List<string>();
        private IApplicationUser _user;
        private IEnumerable<string> AllowedExtensions => new List<string> { ".jpg", ".png", ".jpeg", ".gif" };
        public EfAddImageToBlogPostCommand(BlogContext context,IApplicationUser user) : base(context)
        {
            _user = user;
        }

        public int Id => 2010;

        public string Name => "Add images to blogposts";

        public string Description => "Connect images to blog posts using EF and IFormFile";

        public void Execute(ImageDto x)
        {
                var extension = Path.GetExtension(x.Image.FileName);
                if (!AllowedExtensions.Contains(extension))
                {
                    errors.ToList<string>().Add(extension);
                    throw new InvalidOperationException("Los tip slike..");
                }
                if (!Context.BlogPosts.Any(y => y.Id == x.BlogPostId))
                {
                    errors.ToList<string>().Add("Ne postoji blog post sa identifikatorom [" + x.BlogPostId + "]");
                    throw new EntityNotFoundException(nameof(BlogPost), x.BlogPostId);
                }
                if (!Context.BlogPosts.Any(y => y.Id == x.BlogPostId && y.AuthorId == _user.Id))
                {
                    errors.ToList<string>().Add("Mozete dodavati slike samo na svojim objavama.");
                    throw new ForbiddenUseCaseExecutionException(Name, _user.Email);
                }
            if (!errors.Any())
            {
                var imgAlt = "";
                var guid = Guid.NewGuid().ToString();
                extension = Path.GetExtension(x.Image.FileName);
                imgAlt = Path.GetFileNameWithoutExtension(x.Image.FileName);
                var imeSlike = guid + extension;
                var putanja = Path.Combine("wwwroot", "images", imeSlike);

                using (var stream = new FileStream(putanja, FileMode.Create))
                { x.Image.CopyTo(stream); }
                var image = new Image
                {
                    Src = putanja,
                    Alt = imgAlt
                };
                Context.Images.Add(image);
                Context.SaveChanges();
                var imageId = Context.Images.FirstOrDefault(y => y.Src == putanja);
                var blogPostImage = new BlogPostImage { ImageId = imageId.Id, PostId = x.BlogPostId };
                Context.BlogPostImages.Add(blogPostImage);
                Context.SaveChanges();
            }
                }
            }
        }
    
