using Blog.Application.UseCases.DTO;
using Blog.Application.UseCases.DTO.Base;

namespace Blog.Application.UseCases.Queries
{
    public interface ISearchUserUseCasesQuery : IQuery<BasePagedSearch,PagedResponse<UserUseCaseDto>>
    {
    }
}
