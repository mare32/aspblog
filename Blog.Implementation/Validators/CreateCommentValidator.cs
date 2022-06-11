using Blog.Application.UseCases.DTO;
using Blog.DataAccess;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Implementation.Validators
{
    public class CreateCommentValidator : AbstractValidator<CommentDto>
    {
        BlogContext _context;
        public CreateCommentValidator(BlogContext context)
        {
            _context = context;
            RuleFor( x => x.CommentText).Cascade(CascadeMode.Stop)
                                        .NotEmpty().WithMessage("Polje komentara ne sme ostati prazno");
            RuleFor(x => x.ParentId).Must(parentIdCheck)
                                    .WithMessage("Roditeljski komentar ne postoji");
            RuleFor(x => x.BlogPostId).Must(x => context.BlogPosts.Any(y => y.Id == x))
                                      .WithMessage("Blog Post sa tim identifikatorom ne postoji");
        }
        bool parentIdCheck(int? id)
        {
            if (id == 0 || id == null)
                return true;
            bool parentExists = _context.Comments.Any(y => y.Id == id);
            return parentExists;
        }
    }
}
