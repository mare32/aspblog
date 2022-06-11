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
    public class RegisterUserValidator : AbstractValidator<RegisterDto>
    {
        private BlogContext _context;
        public RegisterUserValidator(BlogContext context)
        {
            _context = context;
            var imePrezimeRegex = @"^[A-Z][a-z]{2,}(\s[A-Z][a-z]{2,})?$";
            RuleFor(x => x.FirstName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Ime ne sme biti prazno")
                .Matches(imePrezimeRegex).WithMessage("Ime nije u ispravnom formatu.");
            RuleFor(x => x.LastName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Prezime ne sme biti prazno")
                .Matches(imePrezimeRegex).WithMessage("Prezime nije u ispravnom formatu.");
            RuleFor(x => x.Username)
                .Cascade(CascadeMode.Stop)
                .MinimumLength(3).WithMessage("Username mora imati makar 3 karaktera")
                .MaximumLength(40).WithMessage("Username ne sme imati vise od 40 karaktera")
                .Must(UsernameNotInUse).WithMessage("Username {PropertyValue} je vec u upotrebi");
            RuleFor(x => x.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Email polje ne sme biti prazno")
                .EmailAddress().WithMessage("Email nije ispravnog formata.")
                .Must(EmailNotInUse).WithMessage("Email {PropertyValue} je vec u upotrebi");
            RuleFor(x => x.Password)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("Lozinka je obavezan podatak.")
                .MinimumLength(4).WithMessage("Lozinka mora imati makar 4 karaktera.")
                .MaximumLength(40).WithMessage("Lozinka ne sme imati vise od 40 karaktera.");
        }
        bool UsernameNotInUse(string username)
        {
            bool exists = _context.Users.Any(x => x.Username == username);

            return !exists;
        }
        bool EmailNotInUse(string email)
        {
            bool exists = _context.Users.Any(x => x.Email == email);

            return !exists;
        }
    }
}
