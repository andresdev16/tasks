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
    internal class GetSummaryQueryHandler : IQueryRequestHandler<GetTaskSummaryQuery, IEnumerable<Domain.Aggregates.TaskAggregate.Task>>
    {
        private readonly EntityFrameworkCoreQueryProvider<TaskContext> queryProvider;

        public GetSummaryQueryHandler(EntityFrameworkCoreQueryProvider<TaskContext> queryProvider)
        {
            this.queryProvider = queryProvider;
        }

        public async Task<IEnumerable<Domain.Aggregates.TaskAggregate.Task>> Handle(GetTaskSummaryQuery query, CancellationToken cancellationToken)
        {
            return await queryProvider.GetQuery<Domain.Aggregates.TaskAggregate.Task>().Skip(query.Offset).Take(query.Limit).ToListAsync(cancellationToken);
        }
    }
}
