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
    public class EfSearchRolesQuery : EfUseCase, ISearchRolesQuery
    {
        public EfSearchRolesQuery(BlogContext context) : base(context)
        {
        }

        public int Id => 2015;

        public string Name => "Search Roles";

        public string Description => "Search roles with a keyword using EF";

        public IEnumerable<RoleDto> Execute(BaseSearch search)
        {
            var response = Context.Roles.AsQueryable();
            if(search.Keyword != null)
            {
                response = response.Where(x => x.Name.Contains(search.Keyword));
            }
            List<RoleDto> uloge = response.Select(x => new RoleDto
            {
                Id = x.Id,
                Name = x.Name
            }).ToList();
            return uloge;
        }
    }
}
