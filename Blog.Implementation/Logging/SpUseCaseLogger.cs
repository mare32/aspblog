using Blog.Application.UseCases;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Implementation.Logging
{
    public class SpUseCaseLogger : IUseCaseLogger
    {
        private string _connString;

        public SpUseCaseLogger(string connString)
        {
            _connString = connString;
        }

        public IEnumerable<UseCaseLog> GetLogs(UseCaseLogSearch search)
        {
            var connection = new SqlConnection(_connString);
            return connection.Query<UseCaseLog>("GetUseCaseLogs",
                                new
                                {
                                    dateFrom = search.DateFrom,
                                    dateTo = search.DateTo,
                                    useCaseName = search.UseCaseName,
                                    username = search.Username
                                },
                                commandType: System.Data.CommandType.StoredProcedure);
        }

        public void Log(UseCaseLog log)
        {
            var connection = new SqlConnection(_connString);

            connection.Query("AddNewLogRecord",
                                new
                                {
                                    useCaseName = log.UseCaseName,
                                    username = log.Username,
                                    userId = log.UserId,
                                    executedAt = log.ExecutedAt,
                                    data = log.Data,
                                    isAuthorized = log.IsAuthorized
                                },
                                commandType: System.Data.CommandType.StoredProcedure);
        }
    }
}
