using Blog.Application.UseCases.DTO.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.UseCases.DTO
{
    public class VoteDto : BaseDto
    {
        public int UserId { get; set; }
        public int VoteType { get; set; }
        public int? BlogPostId { get; set; }
        public int? CommentId { get; set; }
    }
}
