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
    public class CreateCategoryValidator : AbstractValidator<CategoryDto>
    {
        private BlogContext _context;
        public CreateCategoryValidator(BlogContext context)
        {
            RuleFor(x => x.Name)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Naziv je obavezan podatak.")
                .MinimumLength(3).WithMessage("Minimalan broj karaktera je 3.")
                .Must(CategoryNotInUse).WithMessage("Naziv {PropertyValue} je već u upotrebi.");
            _context = context;
            //Func<string,bool>
        }

        private bool CategoryNotInUse(string name)
        {
            var exists = _context.Categories.Any(x => x.Name == name);

            return !exists;
        }
    }
}
