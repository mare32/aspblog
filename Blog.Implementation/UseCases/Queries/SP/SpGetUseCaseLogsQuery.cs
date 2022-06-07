using Blog.Application.UseCases;
using Blog.Application.UseCases.Queries;
using Blog.Implementation.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Implementation.UseCases.Queries.SP
{
    public class SpGetUseCaseLogsQuery : IGetUseCaseLogsQuery
    {
        public int Id => 2026;

        public string Name => "Search Logs";

        public string Description => "Search through use case logs using SP";

        private IUseCaseLogger _logger;
        private SearchUseCaseLogsValidator _validator;
        public SpGetUseCaseLogsQuery(IUseCaseLogger logger, SearchUseCaseLogsValidator validator)
        {
            _logger = logger;
            _validator = validator;
        }

        public IEnumerable<UseCaseLog> Execute(UseCaseLogSearch request)
        {
            _validator.ValidateAndThrow(request);
            return _logger.GetLogs(request);
        }
    }
}
