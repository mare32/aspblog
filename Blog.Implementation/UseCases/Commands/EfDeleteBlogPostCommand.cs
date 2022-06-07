using Blog.Application.Exceptions;
using Blog.Application.UseCases.Commands;
using Blog.DataAccess;
using Blog.Domain;
using Blog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Implementation.UseCases.Commands
{
    public class EfDeleteBlogPostCommand : EfUseCase, IDeleteBlogPostCommand
    {
        IApplicationUser _user;
        public EfDeleteBlogPostCommand(BlogContext context, IApplicationUser user) : base(context)
        {
            _user = user;
        }

        public int Id => 2024;

        public string Name => "Delete BlogPost";

        public string Description => "Delete BlogPost by id using EF";

        public void Execute(int id)
        {
            // ovo sto radim sa authorId-jevima u mnogim klasama je pogresno, refaktorisati
            var blogPost = Context.BlogPosts.FirstOrDefault( x => x.Id == id && x.AuthorId == _user.Id );
            if(blogPost == null)
            {
                throw new EntityNotFoundException(nameof(BlogPost), id);
            }
            var blogPostCategories = Context.BlogPostCategories.Where(x => x.PostId == id);
            Context.BlogPostCategories.RemoveRange(blogPostCategories);
            var blogPostImages = Context.BlogPostImages.Where(x => x.PostId == id);
            Context.BlogPostImages.RemoveRange(blogPostImages);
            foreach(var slika in blogPostImages)
            {
                File.Delete(slika.Image.Src);
                Context.Images.Remove(slika.Image);
            }
            var blogPostVotes = Context.Votes.Where(x => x.PostId == id);
            if (blogPostVotes.Any())
                Context.Votes.RemoveRange(blogPostVotes);
            var comments = Context.Comments.Where(x => x.PostId == id);
            if (comments.Any())
            {
                var commentVotes = Context.Votes.Where(x => comments.Any(y => x.CommentId == y.Id));
                if (commentVotes.Any())
                    Context.Votes.RemoveRange(commentVotes);

                Context.Comments.RemoveRange(comments);
            }
            Context.BlogPosts.Remove(blogPost);
            Context.SaveChanges();

        }
    }
}
