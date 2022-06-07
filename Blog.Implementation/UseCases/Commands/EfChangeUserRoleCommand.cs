using Blog.Application.Exceptions;
using Blog.Application.UseCases.Commands;
using Blog.Application.UseCases.DTO;
using Blog.DataAccess;
using Blog.Domain;
using Blog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Implementation.UseCases.Commands
{
    public class EfChangeUserRoleCommand : EfUseCase, IChangeUserRoleCommand
    {
        private IApplicationUser _user;
        public EfChangeUserRoleCommand(BlogContext context, IApplicationUser user) : base(context)
        {
            _user = user;
        }

        public int Id => 2016;

        public string Name => "Change Role";

        public string Description => "Change user role using EF";

        public void Execute(ChangeRoleDto dto)
        {
            if(dto.UserId == _user.Id)
            {
                throw new Exception("Ne mozete promeniti svoju ulogu.");
            }
            var user = Context.Users.FirstOrDefault(x => x.Id == dto.UserId);
            if(user == null)
            {
                throw new EntityNotFoundException(nameof(User), dto.UserId);
            }
            if(user.RoleId == dto.RoleId)
            {
                throw new Exception("Korisnik vec ima tu ulogu.");
            }
            if(!Context.Roles.Any( x => x.Id == dto.RoleId))
            {
                throw new EntityNotFoundException(nameof(Role), dto.RoleId);
            }
            user.RoleId = dto.RoleId;
            user.UpdatedAt = DateTime.Now;

            Context.Users.Update(user);
            Context.SaveChanges();
        }
    }
}
