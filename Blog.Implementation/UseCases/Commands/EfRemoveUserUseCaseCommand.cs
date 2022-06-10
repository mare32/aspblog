using Blog.Application.Exceptions;
using Blog.Application.UseCases.Commands;
using Blog.Application.UseCases.DTO;
using Blog.DataAccess;
using Blog.Domain;
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
    public class EfRemoveUserUseCaseCommand : EfUseCase, IRemoveUserUseCaseCommand
    {
        IApplicationUser _user;
        UpdateUserUseCaseValidator _validator;
        public EfRemoveUserUseCaseCommand(BlogContext context, IApplicationUser user, UpdateUserUseCaseValidator validator) : base(context)
        {
            _user = user;
            _validator = validator;
        }

        public int Id => 2032;

        public string Name => "Remove UserUseCase";

        public string Description => "Remove UserUseCase using EF";

        public void Execute(UpdateUserUseCaseDto dto)
        {
            _validator.ValidateAndThrow(dto);
            bool userUseCaseNePostoji = !Context.UserUseCases.Any( x => x.UserId == dto.UserId && x.CaseId == dto.UseCaseId);
            if (userUseCaseNePostoji)
                throw new EntityNotFoundException(nameof(UserUseCase), dto.UseCaseId);
            UserUseCase userUseCaseToRemove = Context.UserUseCases.FirstOrDefault(x => x.UserId == dto.UserId && x.CaseId == dto.UseCaseId);
            Context.UserUseCases.Remove(userUseCaseToRemove);
            Context.SaveChanges();
        }
    }
}
