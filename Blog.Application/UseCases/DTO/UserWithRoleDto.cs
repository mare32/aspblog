using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.UseCases.DTO
{
    public class UserWithRoleDto : UserDto
    {
        public string Role { get; set; }
    }
}
