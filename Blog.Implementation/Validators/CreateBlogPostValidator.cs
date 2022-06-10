using Blog.Application.UseCases.DTO;
using Blog.DataAccess;
using Blog.Domain;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Implementation.Validators
{
    public class CreateBlogPostValidator : AbstractValidator<CreateBlogPostDto>
    {
        private BlogContext _context;
        private IApplicationUser _user;
        public CreateBlogPostValidator(BlogContext context, IApplicationUser user)
        {
            _context = context;
            _user = user;
            RuleFor(x => x.Title)
                                .Cascade(CascadeMode.Stop)
                                .NotEmpty().WithMessage("Naslov ne sme biti prazan.")
                                .MinimumLength(3).WithMessage("Naslov mora imati makar 3 karaktera")
                                .MaximumLength(50).WithMessage("Naslov mora imati maksimum 50 karaktera")
                                .Must(TitleIsUniqueForThisUser).WithMessage("Vec imate objavu sa istim naslovom");
            RuleFor(x => x.BlogPostContent)
                                .Cascade(CascadeMode.Stop)
                                .NotEmpty().WithMessage("Sadrzaj objave ne sme biti prazan.")
                                .MinimumLength(3).WithMessage("Sadrzaj objave mora imati makar 3 karaktera");
            RuleFor(x => x.CategoryIds)
                                .NotEmpty()
                                .WithMessage("Molim vas izaberite makar jednu kategoriju.");
            RuleForEach(x => x.CategoryIds).Must(y => _context.Categories.Any(h => h.Id == y))
                                            .WithMessage("Kategorija sa identifikatorom {PropertyValue} ne postoji.");
            _user = user;
        }
        bool TitleIsUniqueForThisUser(string title)
        {
            var thisUser = _context.Users.FirstOrDefault(x => x.Id == _user.Id);
            bool exists = thisUser.BlogPosts.Any( x => x.Title == title);
            return !exists;
        }
    }
}
