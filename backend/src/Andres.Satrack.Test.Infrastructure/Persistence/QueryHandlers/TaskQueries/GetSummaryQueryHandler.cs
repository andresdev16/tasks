using Andres.Satrack.Test.Application.Queries;
using Microsoft.EntityFrameworkCore;
using SharedKernel.Application.Cqrs.Queries;
using SharedKernel.Infrastructure.Data.EntityFrameworkCore.Queries;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andres.Satrack.Test.Infrastructure.Persistence.QueryHandlers.TaskQueries
{
    internal class GetSummaryQueryHandler : IQueryRequestHandler<GetTaskSummaryQuery, Summary>
    {
        private readonly EntityFrameworkCoreQueryProvider<TaskContext> queryProvider;

        public GetSummaryQueryHandler(EntityFrameworkCoreQueryProvider<TaskContext> queryProvider)
        {
            this.queryProvider = queryProvider;
        }

        public async Task<Summary> Handle(GetTaskSummaryQuery query, CancellationToken cancellationToken)
        {
            var tasks = await queryProvider.GetQuery<Domain.Aggregates.TaskAggregate.Task>().Skip(query.Offset).Take(query.Limit).ToListAsync(cancellationToken);
            int total = await queryProvider.GetQuery<Domain.Aggregates.TaskAggregate.Task>().CountAsync();

            return new Summary { Tasks = tasks, Total = total };
        }
    }
}
