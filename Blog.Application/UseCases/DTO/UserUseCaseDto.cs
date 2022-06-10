using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.UseCases.DTO
{
    public class UserUseCaseDto
    {
        public string UseCaseName { get; set; }
        public int UseCaseId { get; set; }
        public IEnumerable<UserUserUseCaseDto> Users { get; set; }
    }
    public class UserUserUseCaseDto
    {
        public string UserName { get; set; }
    }
}
