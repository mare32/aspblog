using Blog.Application.UseCases.Commands;
using Blog.Application.UseCases.DTO;
using Blog.DataAccess;
using Blog.Domain;
using Blog.Implementation.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Implementation.UseCases.Commands
{
    public class EfUpdateUserProfileCommand : EfUseCase, IUpdateUserProfileCommand
    {
        IApplicationUser _user;
        UpdateUserProfileValidator _validator;
        public EfUpdateUserProfileCommand(BlogContext context, IApplicationUser user, UpdateUserProfileValidator validator) : base(context)
        {
            _user = user;
            _validator = validator;
        }

        public int Id => 2020;

        public string Name => "Update User Profile";

        public string Description => "Update User Profile using EF";

        public void Execute(UpdateUserProfileDto dto)
        {
            
           var user = Context.Users.FirstOrDefault(x => x.Id == _user.Id);
            if(dto.Username != null)
            {
                user.Username = dto.Username;
            }
            if(dto.Email != null)
            {
                user.Email = dto.Email;
            }
            if(dto.FirstName != null)
            {
                user.FirstName = dto.FirstName;
            }
            if(dto.LastName != null)
            {
                user.LastName = dto.LastName;
            }
            if(dto.Password != null)
            {
                var hash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
                user.Password = hash;
            }
            user.UpdatedAt = DateTime.Now;
            _validator.ValidateAndThrow(user);
            Context.Users.Update(user);
            Context.SaveChanges();
        }
    }
}
