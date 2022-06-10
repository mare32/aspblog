using Blog.Application.UseCases.DTO;
using Blog.DataAccess;
using Blog.Domain.Entities;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Implementation.Validators
{
    public class PatchBlogPostValidator : AbstractValidator<BlogPost>
    {
        public PatchBlogPostValidator()
        {
            RuleFor(x => x.Title)
                                .Cascade(CascadeMode.Stop)
                                .NotEmpty().WithMessage("Naslov ne sme biti prazan.")
                                .MinimumLength(3).WithMessage("Naslov mora imati makar 3 karaktera")
                                .MaximumLength(50).WithMessage("Naslov mora imati maksimum 50 karaktera");
            RuleFor(x => x.BlogPostContent)
                                .Cascade(CascadeMode.Stop)
                                .NotEmpty().WithMessage("Sadrzaj objave ne sme biti prazan.")
                                .MinimumLength(3).WithMessage("Sadrzaj objave mora imati makar 3 karaktera");
        }
    }
}
