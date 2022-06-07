using Blog.Application.UseCases.Commands;
using Blog.Application.UseCases.DTO;
using Blog.DataAccess;
using Blog.Domain.Entities;
using Blog.Implementation.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Implementation.UseCases.Commands
{
    public class EfUpdateUserUseCasesCommand : EfUseCase, IUpdateUserUseCasesCommand
    {
        UpdateUserUseCasesValidator _validator;
        public EfUpdateUserUseCasesCommand(BlogContext context, UpdateUserUseCasesValidator validator) : base(context)
        {
            _validator = validator;
        }

        public int Id => 2018;

        public string Name => "Update User UseCases";

        public string Description => "Update User UseCases using EF";

        public void Execute(UpdateUserUseCasesDto request)
        {
            _validator.ValidateAndThrow(request);
            var userUseCases = Context.UserUseCases.Where(x => x.UserId == request.UserId).ToList();
            Context.RemoveRange(userUseCases);
            var useCasesToAdd = request.UseCaseIds.Select(x => new UserUseCase
            {
                CaseId = x,
                UserId = request.UserId
            });
            Context.UserUseCases.AddRange(useCasesToAdd);
            Context.SaveChanges();
        }
    }
}
