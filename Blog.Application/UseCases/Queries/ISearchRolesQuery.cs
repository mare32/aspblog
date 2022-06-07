using Blog.Application.UseCases.DTO;
using Blog.Application.UseCases.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.UseCases.Queries
{
    public interface ISearchRolesQuery : IQuery<BaseSearch, IEnumerable<RoleDto>>
    {
    }
}
