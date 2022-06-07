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
    public class EfCreateCommentValidator : AbstractValidator<CommentDto>
    {
        public EfCreateCommentValidator(BlogContext context)
        {
            RuleFor( x => x.CommentText).Cascade(CascadeMode.Stop)
                                        .NotEmpty().WithMessage("Polje komentara ne sme ostati prazno");
            RuleFor(x => x.ParentId).Must(x => context.Comments.Any(y => y.Id == x))
                                    .WithMessage("Roditeljski komentar ne postoji");
            RuleFor(x => x.BlogPostId).Must(x => context.BlogPosts.Any(y => y.Id == x))
                                      .WithMessage("Blog Post sa tim identifikatorom ne postoji");
        }
    }
}
