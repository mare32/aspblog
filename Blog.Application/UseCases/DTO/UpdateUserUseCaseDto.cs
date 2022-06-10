using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.UseCases.DTO
{
    public class UpdateUserUseCaseDto
    {
        public int UserId { get; set; }
        public int UseCaseId { get; set; }
    }
}
