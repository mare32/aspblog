using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.UseCases.DTO
{
    public class UpdateUserUseCasesDto
    {
        public int UserId { get; set; }
        public IEnumerable<int> UseCaseIds { get; set; }
    }
}
