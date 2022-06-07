using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Domain.Entities
{
    public class UseCase : Entity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public virtual ICollection<UserUseCase> UserUseCases { get; set; }

    }
}
