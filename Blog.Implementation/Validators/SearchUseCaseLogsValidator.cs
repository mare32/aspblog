using Blog.Application.UseCases;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Implementation.Validators
{
    public class SearchUseCaseLogsValidator : AbstractValidator<UseCaseLogSearch>
    {
        public SearchUseCaseLogsValidator()
        {
            RuleFor(x => x.DateFrom)
                        .NotEmpty().WithMessage("DateFrom je obavezno polje")
                        .Must((search, datefrom) =>
                        {
                            if(!search.DateTo.HasValue)
                            {
                                return false;
                            }
                            var timeSpan = (search.DateTo - search.DateFrom).Value;

                            return timeSpan.TotalDays <= 15;
                        });
            RuleFor(x => x.DateTo)
                        .NotEmpty().WithMessage("DateTo je obavezno polje");
        }
    }
}
