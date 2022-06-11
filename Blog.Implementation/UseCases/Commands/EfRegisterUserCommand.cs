using Blog.Application.Emails;
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
    public class EfRegisterUserCommand : EfUseCase, IRegisterUserCommand
    {
        private RegisterUserValidator _validator;
        private readonly IEmailSender _sender;
        public EfRegisterUserCommand(BlogContext context, RegisterUserValidator validator, IEmailSender sender) : base(context)
        {
            _sender = sender;
            _validator = validator;
        }
        public int Id => 1002;

        public string Name => "Register";

        public string Description => "Register user using Entity Framework";

        public void Execute(RegisterDto request)
        {
            _validator.ValidateAndThrow(request);
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(request.Password);
            var user = new User
            {
                Username = request.Username,
                Email = request.Email,
                Password = hashedPassword,
                FirstName = request.FirstName,
                LastName = request.LastName
            };
            Context.Users.Add(user);
            Context.SaveChanges();

            var addedUser = Context.Users.Where(x => x.Username == request.Username).FirstOrDefault();
            var usecases = new List<int>{ 1,2,2002,2005,2006,2007,2010,2011,2012,2014,2017,2019,2020,2021,2022,2023,2024,2028, 2029, 2033 }; // hardkodovano
            var userUsecases = usecases.Select(x => new UserUseCase
            {
                CaseId = x,
                UserId = addedUser.Id
            }).ToList();
            Context.UserUseCases.AddRange(userUsecases);
            Context.SaveChanges();


            // Mejl svakako ne radi, pa da ne usporava aplikaciju
            //_sender.Send(new MessageDto
            //{
            //    Title = "Successful registration!",
            //    To = request.Email,
            //    Body = "Welcome to the blog kind " + request.Username + "\n Click the link down below to activate your account\n*Generated Activation Link*"
            //});
        }
    }
}
