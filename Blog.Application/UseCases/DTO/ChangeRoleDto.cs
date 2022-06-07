using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.UseCases.DTO
{
    public class ChangeRoleDto
    {
        public int RoleId { get; set; }
        public int UserId { get; set; }
    }
}
