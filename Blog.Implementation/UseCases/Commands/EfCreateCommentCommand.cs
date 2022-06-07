using Blog.Application.Exceptions;
using Blog.Application.UseCases.Commands;
using Blog.Application.UseCases.DTO;
using Blog.DataAccess;
using Blog.Domain;
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
    public class EfCreateCommentCommand : EfUseCase, ICreateCommentCommand
    {
        private CreateCommentValidator _validator;
        private IApplicationUser _user;
        public EfCreateCommentCommand(BlogContext context,
                                        CreateCommentValidator validator,
                                        IApplicationUser user) : base(context)
        {
            _validator = validator;
            _user = user;
        }

        public int Id => 2012;

        public string Name => "Create comment";

        public string Description => "Create comment using EF";

        public void Execute(CommentDto dto)
        {
            int? parentId = null;
            _validator.ValidateAndThrow(dto);
            if(dto.ParentId.HasValue)
                parentId = dto.ParentId.Value;
            var newComment = new Comment
            {
                UserId = _user.Id,
                CommentText = dto.CommentText,
                ParentId = parentId,
                PostId = dto.BlogPostId
            };
            Context.Comments.Add(newComment);
            Context.SaveChanges();
        }
    }
}
