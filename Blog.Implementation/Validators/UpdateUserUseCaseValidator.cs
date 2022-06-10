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
    public class UpdateUserUseCaseValidator : AbstractValidator<UpdateUserUseCaseDto>
    {
        BlogContext _context;
        public UpdateUserUseCaseValidator(BlogContext context)
        {
            _context = context;
            RuleFor(x => x.UseCaseId).Cascade(CascadeMode.Stop)
                                     .NotEmpty().WithMessage("UseCaseId je obavezan podatak")
                                     .Must(useCasePostoji).WithMessage("UseCase sa identifikatorom: {PropertyValue} ne postoji.");
            RuleFor(x => x.UserId).Cascade(CascadeMode.Stop)
                                     .NotEmpty().WithMessage("UserId je obavezan podatak")
                                     .Must(userPostoji).WithMessage("User sa identifikatorom: {PropertyValue} ne postoji.");
        }
        bool useCasePostoji(int caseId)
        {
            bool postoji = _context.UseCases.Any(x => x.Id == caseId);
            return postoji;
        }
        bool userPostoji(int userId)
        {
            bool postoji = _context.Users.Any(x => x.Id == userId);
            return postoji;
        }
    }
}
