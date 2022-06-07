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
    public class UpdateBlogPostCategoriesValidator : AbstractValidator<UpdateBlogPostCategoriesDto>
    {
        private BlogContext _context;
        public UpdateBlogPostCategoriesValidator(BlogContext context)
        {
            RuleFor(x => x.BlogPostId).Must(x => _context.BlogPosts.Any(y => y.Id == x)).WithMessage("Blog post id:{PropertyValue} ne postoji.");
            RuleForEach(x => x.CategoryIds).Must(y => _context.Categories.Any(h => h.Id == y))
                                           .WithMessage("Kategorija sa identifikatorom {PropertyValue} ne postoji.");
            _context = context;
        }
    }
}
