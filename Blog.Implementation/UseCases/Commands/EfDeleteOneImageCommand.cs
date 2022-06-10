using Blog.Application.Exceptions;
using Blog.Application.UseCases.Commands;
using Blog.DataAccess;
using Blog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Implementation.UseCases.Commands
{
    public class EfDeleteOneImageCommand : EfUseCase, IDeleteOneImageCommand
    {
        public EfDeleteOneImageCommand(BlogContext context) : base(context)
        {
        }

        public int Id => 2021;

        public string Name => "Delete One Image";

        public string Description => "Delete single image using EF";

        public void Execute(int imageId)
        {
            var image = Context.Images.FirstOrDefault(x => x.Id == imageId);
            if(Context.BlogPosts.Any( x => x.CoverImage == imageId))
            {
                throw new ValidationConflictException("Ova slika se koristi kao CoverImage za neku objavu, prvo to promenite");
            }
            if (!Context.Images.Any(x => x.Id == imageId))
            {
                throw new EntityNotFoundException(nameof(Image),imageId);
            }
            File.Delete(image.Src);
            var blogPostImgToDelete = Context.BlogPostImages.FirstOrDefault(x => x.ImageId == imageId);
            Context.BlogPostImages.Remove(blogPostImgToDelete);
            Context.Images.Remove(image);
            Context.SaveChanges();
        }
    }
}
