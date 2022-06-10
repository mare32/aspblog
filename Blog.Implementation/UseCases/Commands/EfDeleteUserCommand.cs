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
    public class EfDeleteUserCommand : EfUseCase, IDeleteUserCommand
    {
        IApplicationUser _user;
        public EfDeleteUserCommand(BlogContext context,IApplicationUser user) : base(context)
        {
            _user = user;
        }

        public int Id => 2025;

        public string Name => "Delete User";

        public string Description => "Delete User by id using EF";

        public void Execute(int request)
        {
            var user = Context.Users.FirstOrDefault(x => x.Id == request);
            if(user == null)
            {
                throw new EntityNotFoundException(nameof(User), request);
            }
            if(user.Id == _user.Id)
            {
                throw new Exception("Brisanje sopstvenog naloga nije moguce.");
            }
           
            if(user.BlogPosts.Any())
            {
                foreach(var blogPost in user.BlogPosts)
                {
                    var id = blogPost.Id;
                    var blogPostCategories = Context.BlogPostCategories.Where(x => x.PostId == id);
                    Context.BlogPostCategories.RemoveRange(blogPostCategories);
                    var blogPostImages = Context.BlogPostImages.Where(x => x.PostId == id);
                    Context.BlogPostImages.RemoveRange(blogPostImages);
                    foreach (var slika in blogPostImages)
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
                }
            }
            
            
            if(user.Votes.Any())
                Context.Votes.RemoveRange(user.Votes);
            if (user.Comments.Any())
                Context.Comments.RemoveRange(user.Comments);
            Context.UserUseCases.RemoveRange(user.UserUseCases);
            Context.Users.Remove(user);
            Context.SaveChanges();
        }
    }
}
