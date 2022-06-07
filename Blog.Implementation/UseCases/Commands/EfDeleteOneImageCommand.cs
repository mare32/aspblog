using Blog.Application.UseCases.Commands;
using Blog.DataAccess;
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
            // validator
            var image = Context.Images.FirstOrDefault(x => x.Id == imageId);
            if(Context.BlogPosts.Any( x => x.CoverImage == imageId))
            {
                throw new Exception("Ova slika se koristi kao CoverImage za neku objavu, prvo to promenite");
            }
            File.Delete(image.Src);
            var blogPostImgToDelete = Context.BlogPostImages.FirstOrDefault(x => x.ImageId == imageId);
            Context.BlogPostImages.Remove(blogPostImgToDelete);
            Context.Images.Remove(image);
            Context.SaveChanges();
        }
    }
}
