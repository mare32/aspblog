using Blog.Application.UseCases.DTO;
using Blog.Application.UseCases.DTO.Base;
using Blog.Application.UseCases.Queries;
using Blog.DataAccess;
using Blog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Implementation.UseCases.Queries.Ef
{
    public class EfSearchUsersQuery : EfUseCase, ISearchUsersQuery
    {
        public EfSearchUsersQuery(BlogContext context) : base(context)
        {
        }

        public int Id => 2003;

        public string Name => "Search users";

        public string Description => "Search users using EF";

        public PagedResponse<UserWithRoleDto> Execute(BasePagedSearch request)
        {
            var query = Context.Users.AsQueryable();
            if (!string.IsNullOrEmpty(request.Keyword))
            {
              query = query.Where(x => x.Username.Contains(request.Keyword) ||
                                                  x.Email.Contains(request.Keyword) ||
                                                  x.FirstName.Contains(request.Keyword) ||
                                                  x.LastName.Contains(request.Keyword));
            }

            if (request.PerPage == null || request.PerPage < 1)
            {
                request.PerPage = 5;
            }

            if (request.Page == null || request.Page < 1)
            {
                request.Page = 1;
            }

            var toSkip = (request.Page.Value - 1) * request.PerPage.Value;

            var response = new PagedResponse<UserWithRoleDto>();
            response.TotalCount = query.Count();
            response.Data = query.Skip(toSkip).Take(request.PerPage.Value).Select(x => new UserWithRoleDto
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Username = x.Username,
                Email = x.Email,
                Role = x.Role.Name
            }).ToList();
            response.CurrentPage = request.Page.Value;
            response.ItemsPerPage = request.PerPage.Value;

            return response;
        }
    }
}
