using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.UseCases.DTO
{
    public class SearchVoteDto : VoteDto
    {
        public string BlogTitle { get; set; }
        public string CommentText { get; set; }
    }
}
