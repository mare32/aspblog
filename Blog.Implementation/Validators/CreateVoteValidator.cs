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
    public class CreateVoteValidator : AbstractValidator<VoteDto>
    {
        public CreateVoteValidator()
        {
            RuleFor(x => x.VoteType).Cascade(CascadeMode.Stop)
                                     .NotEmpty().WithMessage("Polje VoteType ne sme ostati prazno")
                                     .Must(x => x == 1 || x == 2).WithMessage("Vrednost VoteType polja mora biti 1(like) ili 2(dislike)");
                          
        }
    }
}
