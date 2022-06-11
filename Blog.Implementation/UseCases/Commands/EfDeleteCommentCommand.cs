using Blog.Application.Exceptions;
using Blog.Application.UseCases.Commands;
using Blog.DataAccess;
using Blog.Domain;
using Blog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Implementation.UseCases.Commands
{
    public class EfDeleteCommentCommand : EfUseCase, IDeleteCommentCommand
    {
        IApplicationUser _user;
        public EfDeleteCommentCommand(BlogContext context, IApplicationUser user) : base(context)
        {
            _user = user;
        }

        public int Id => 2022;

        public string Name => "Delete comment";

        public string Description => "Delete comment with all of its subcomments using EF";

        public void Execute(int commentId)
        {
             var comment = Context.Comments.FirstOrDefault(c => c.Id == commentId && c.UserId == _user.Id);
            if(comment == null)
            {
                throw new EntityNotFoundException(nameof(Comment), commentId);
            }
            var childComments = Context.Comments.Where(x => x.ParentId == commentId);
            if(childComments.Any())
            Context.Comments.RemoveRange(childComments);
            Context.Comments.Remove(comment);
            Context.SaveChanges();
        }
    }
}
