using Blog.Application.UseCases.DTO;
using Blog.Application.UseCases.DTO.Base;
using Blog.Application.UseCases.Queries;
using Blog.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Implementation.UseCases.Queries.Ef
{
    public class EfSearchUserUseCasesQuery : EfUseCase, ISearchUserUseCasesQuery
    {
        public EfSearchUserUseCasesQuery(BlogContext context) : base(context)
        {
        }

        public int Id => 2030;

        public string Name => "Search UserUseCases";

        public string Description => "Search UserUseCases by keyword using EF";

        public PagedResponse<UserUseCaseDto> Execute(BasePagedSearch request)
        {
            var query = Context.UseCases.Include(x => x.UserUseCases).ThenInclude(x => x.User).AsQueryable();

            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(x => x.Name.Contains(request.Keyword) || x.Id.ToString() == request.Keyword 
                || x.UserUseCases.Any(y => y.User.Username.Contains(request.Keyword)));
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

            var response = new PagedResponse<UserUseCaseDto>();
            response.TotalCount = query.Count();
            response.Data = query.Skip(toSkip).Take(request.PerPage.Value).Select(x => new UserUseCaseDto
            {
                UseCaseId = x.Id,
                UseCaseName = x.Name,
                Users = x.UserUseCases.Select( x => new UserUserUseCaseDto
                {
                    UserName = x.User.Username
                }).ToList()
            }).ToList();
            response.CurrentPage = request.Page.Value;
            response.ItemsPerPage = request.PerPage.Value;

            return response;
        }
    }
}
