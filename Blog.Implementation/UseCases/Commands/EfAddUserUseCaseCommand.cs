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
    public class EfAddUserUseCaseCommand : EfUseCase, IAddUserUseCaseCommand
    {
        IApplicationUser _user;
        UpdateUserUseCaseValidator _validator;
        public EfAddUserUseCaseCommand(BlogContext context, IApplicationUser user, UpdateUserUseCaseValidator validator) : base(context)
        {
            _user = user;
            _validator = validator;
        }
        public int Id => 2031;

        public string Name => "Add UserUseCase";

        public string Description => "Add UserUseCase using EF";

        public void Execute(UpdateUserUseCaseDto dto)
        {
            _validator.ValidateAndThrow(dto);
            bool userUseCaseVecPostoji = Context.UserUseCases.Any(x => x.UserId == dto.UserId && x.CaseId == dto.UseCaseId);
            if(userUseCaseVecPostoji)
                throw new ValidationConflictException("Korisnik vec ima izabranu privilegiju");

            UserUseCase userUseCaseToAdd = new UserUseCase
            {
                UserId = dto.UserId,
                CaseId = dto.UseCaseId
            };
            Context.UserUseCases.Add(userUseCaseToAdd);
            Context.SaveChanges();
        }
    }
}
