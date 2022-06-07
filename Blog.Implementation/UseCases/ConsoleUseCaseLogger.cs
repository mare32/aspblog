using Blog.Application.UseCases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Implementation.UseCases
{
    public class ConsoleUseCaseLogger : IUseCaseLogger
    {
        public IEnumerable<UseCaseLog> GetLogs(UseCaseLogSearch search)
        {
            throw new NotImplementedException();
        }

        public void Log(UseCaseLog log)
        {
            Console.WriteLine($"UseCase: {log.UseCaseName}, User: {log.Username}, {log.ExecutedAt}, Authorized: {log.IsAuthorized}");
            Console.WriteLine($"Use Case Data: " + log.Data);
        }
    }
}
