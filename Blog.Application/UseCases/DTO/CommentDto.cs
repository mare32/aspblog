using Blog.Application.UseCases.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.UseCases.DTO
{
    public class CommentDto : BaseDto
    {
        public string CommentText { get; set; }
        public int BlogPostId { get; set; }
        public int? AuthorId { get; set; }
        public int? ParentId { get; set; }
    }
    public class CommentWithVotesDto : CommentDto
    {
        public int TotalVotes { get; set; }
        public int DownVotes { get; set; }
        public int UpVotes { get; set; }
        public int VoteScore { get; set; }
    }
}
