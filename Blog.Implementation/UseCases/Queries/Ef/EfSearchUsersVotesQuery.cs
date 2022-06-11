using Blog.Application.UseCases.DTO;
using Blog.Application.UseCases.DTO.Base;
using Blog.Application.UseCases.Queries;
using Blog.DataAccess;
using Blog.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Implementation.UseCases.Queries.Ef
{
    public class EfSearchUsersVotesQuery : EfUseCase, ISearchUsersVotesQuery
    {
        IApplicationUser _user;
        public EfSearchUsersVotesQuery(BlogContext context, IApplicationUser user) : base(context)
        {
            _user = user;
        }

        public int Id => 2033;

        public string Name => "Search users votes";

        public string Description => "Search votes of a logged user using EF";

        public PagedResponse<SearchVoteDto> Execute(BasePagedSearch search)
        {

            var query = Context.Votes.Include( x=>x.BlogPost).Include( x => x.Comment).AsQueryable();

            if (!string.IsNullOrEmpty(search.Keyword))
            {
                query = query.Where(x => x.UserId == _user.Id && ( x.BlogPost.Title.Contains(search.Keyword) || x.Comment.CommentText.Contains(search.Keyword)));
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

            var response = new PagedResponse<SearchVoteDto>();
            response.TotalCount = query.Count();
            response.Data = query.Skip(toSkip).Take(search.PerPage.Value).Select(x => new SearchVoteDto
            {
                Id = x.Id,
                CommentId = x.CommentId,
                BlogPostId = x.PostId,
                CommentText = x.Comment.CommentText,
                BlogTitle = x.BlogPost.Title,
                UserId = _user.Id,
                VoteType = x.TypeId
            }).ToList();
            response.CurrentPage = search.Page.Value;
            response.ItemsPerPage = search.PerPage.Value;

            return response;
        }
    }
}
