using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Entities
{
    public class UserUseCase
    {
        public int UserId { get; set; }
        public int CaseId { get; set; }

        public virtual User User { get; set; }
        public virtual UseCase UseCase { get; set; }
    }
}
