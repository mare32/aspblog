using Blog.Application.Exceptions;
using Blog.Application.UseCases.DTO;
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
    public class EfShowCommentsQuery : EfUseCase, IShowCommentsQuery
    {
        public EfShowCommentsQuery(BlogContext context) : base(context)
        {
        }

        public int Id => 2017;

        public string Name => "Show comments";

        public string Description => "Show comments by post id using EF";

        public IEnumerable<CommentWithVotesDto> Execute(int postId)
        {
            var post = Context.BlogPosts.FirstOrDefault(x => x.Id == postId);
            if(post == null)
            {
                throw new EntityNotFoundException(nameof(BlogPost), postId);
            }
            var comments = post.Comments.Select(x => new CommentWithVotesDto
            {
                Id = x.Id,
                AuthorId = x.User.Id,
                ParentId = x.ParentId,
                CommentText = x.CommentText,
                BlogPostId = x.PostId,
                TotalVotes = x.Votes.Count,
                UpVotes = x.Votes.Where( y => y.TypeId == 1).Count(),
                DownVotes = x.Votes.Where(y => y.TypeId == 2).Count(),
                VoteScore = x.Votes.Where(y => y.TypeId == 1).Count() - x.Votes.Where(y => y.TypeId == 2).Count()
            }).ToList();
            return comments;
        }
    }
}
