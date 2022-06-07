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
    public class EfGetCategoriesQuery : EfUseCase, IGetCategoriesQuery
    {
        public EfGetCategoriesQuery(BlogContext context) : base(context)
        {
        }

        public int Id => 1;

        public string Name => "Search Categories";

        public string Description => "Search categories using Entity Framework";

        public PagedResponse<CategoryDto> Execute(BasePagedSearch search)
        {
            var query = Context.Categories.AsQueryable();

            if (!string.IsNullOrEmpty(search.Keyword))
            {
                query = query.Where(x => x.Name.Contains(search.Keyword));
            }

            if (search.PerPage == null || search.PerPage < 1)
            {
                search.PerPage = 15;
            }

            if (search.Page == null || search.Page < 1)
            {
                //search.PerPage = 1;
                search.Page = 1;
            }

            // 100
            // perPage = 15
            // 3 treba preskociti => 30
            // 2 => treba prekosiciti 15
            // 5 => 60

            //Algoritam za preskakanje (Page - 1) * PerPage

            //1 -> (1 - 1) * 15 => 0
            //2 -> (2 - 1) * 15 => 15

            var toSkip = (search.Page.Value - 1) * search.PerPage.Value;

            var response = new PagedResponse<CategoryDto>();
            response.TotalCount = query.Count();
            response.Data = query.Skip(toSkip).Take(search.PerPage.Value).Select(x => new CategoryDto
            {
                Name = x.Name,
                Id = x.Id
            }).ToList();
            response.CurrentPage = search.Page.Value;
            response.ItemsPerPage = search.PerPage.Value;

            return response;
        }
    }
}
