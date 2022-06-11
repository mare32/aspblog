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
            // potencijalan validator
            if(dto.UserId == _user.Id)
            {
                throw new ValidationConflictException("Ne mozete promeniti svoju ulogu.");
            }
            var user = Context.Users.FirstOrDefault(x => x.Id == dto.UserId);
            if(user == null)
            {
                throw new EntityNotFoundException(nameof(User), dto.UserId);
            }
            if(user.RoleId == dto.RoleId)
            {
                throw new ValidationConflictException("Korisnik vec ima tu ulogu.");
            }
            if(!Context.Roles.Any( x => x.Id == dto.RoleId))
            {
                throw new EntityNotFoundException(nameof(Role), dto.RoleId);
            }
            user.RoleId = dto.RoleId;
            user.UpdatedAt = DateTime.Now;
            if(dto.RoleId == 1)
            {
                var userUseCases = user.UserUseCases;
                Context.UserUseCases.RemoveRange(userUseCases);
                var adminUseCases = new List<int> {1,2,2002,2003,2004,2005,2006,2007,2010,2011,2012,2014,2016,2017,2018,2019,2020,2021,2022,2023,2024,2025,2026,2027,2028,2029, 2030,2031,2032,2033 }; // moze i da se prodje kroz sve usecase-ove i svi osim registracije da se dodaju adminu
                var newUserUsecases = adminUseCases.Select(x => new UserUseCase
                {
                    CaseId = x,
                    UserId = dto.UserId
                }).ToList();
                Context.UserUseCases.AddRange(newUserUsecases);
            }
            else
            {
                var userUseCases = user.UserUseCases;
                Context.UserUseCases.RemoveRange(userUseCases);
                var regularUseCases = new List<int> { 1, 2, 2002, 2005, 2006, 2007, 2010, 2011, 2012, 2014, 2017, 2019, 2020, 2021, 2022, 2023, 2024,2028,2029,2033 };
                var newUserUsecases = regularUseCases.Select(x => new UserUseCase
                {
                    CaseId = x,
                    UserId = dto.UserId
                }).ToList();
                Context.UserUseCases.AddRange(newUserUsecases);
            }
            Context.Users.Update(user);
            Context.SaveChanges();
        }
    }
}
