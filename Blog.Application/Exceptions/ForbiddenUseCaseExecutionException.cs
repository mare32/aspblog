using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Application.Exceptions
{
    public class ForbiddenUseCaseExecutionException : Exception
    {
        public ForbiddenUseCaseExecutionException(string useCase, string user) :
            base($"{user} has tried to [{useCase}] without being authorized.")
        {

        }
    }
}
