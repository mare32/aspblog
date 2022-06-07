using Blog.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Implementation.UseCases
{
    public class EfUseCase
    {
        protected EfUseCase(BlogContext context)
        {
            Context = context;
        }

        protected BlogContext Context { get; }
    }
}
