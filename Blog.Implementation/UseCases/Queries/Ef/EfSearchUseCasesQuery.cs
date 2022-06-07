using Blog.Application.UseCases.DTO;
using Blog.Application.UseCases.DTO.Base;
using Blog.Application.UseCases.Queries;
using Blog.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Implementation.UseCases.Queries.Ef
{
    public class EfSearchUseCasesQuery : EfUseCase, ISearchUseCasesQuery
    {
        public EfSearchUseCasesQuery(BlogContext context) : base(context)
        {
        }

        public int Id => 2027;

        public string Name => "Search UseCases";

        public string Description => "Search UseCases,paged,using EF";

        public PagedResponse<UseCaseDto> Execute(BasePagedSearch search)
        {
            var query = Context.UseCases.AsQueryable();

            if (!string.IsNullOrEmpty(search.Keyword))
            {
                query = query.Where(x => x.Name.Contains(search.Keyword) || x.Description.Contains(search.Keyword));
            }

            if (search.PerPage == null || search.PerPage < 1)
            {
                search.PerPage = 5;
            }

            if (search.Page == null || search.Page < 1)
            {
                search.Page = 1;
            }

            var toSkip = (search.Page.Value - 1) * search.PerPage.Value;

            var response = new PagedResponse<UseCaseDto>();
            response.TotalCount = query.Count();
            response.Data = query.Skip(toSkip).Take(search.PerPage.Value).Select(x => new UseCaseDto
            {
                Id = x.Id,
                UseCaseName = x.Name,
                Description = x.Description
            }).ToList();
            response.CurrentPage = search.Page.Value;
            response.ItemsPerPage = search.PerPage.Value;

            return response;
        }
    }
}
