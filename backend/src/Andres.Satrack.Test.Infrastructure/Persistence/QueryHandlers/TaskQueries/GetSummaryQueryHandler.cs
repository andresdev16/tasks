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
    internal class GetSummaryQueryHandler : IQueryRequestHandler<GetTaskSummaryQuery, IEnumerable<Task>>
    {
        private readonly EntityFrameworkCoreQueryProvider<TaskContext> queryProvider;

        public GetSummaryQueryHandler(EntityFrameworkCoreQueryProvider<TaskContext> queryProvider)
        {
            this.queryProvider = queryProvider;
        }

        public async Task<IEnumerable<Task>> Handle(GetTaskSummaryQuery query, CancellationToken cancellationToken)
        {
            return await queryProvider.GetQuery<Task>().Skip(query.Offset).Take(query.Limit).ToListAsync(cancellationToken);
        }
    }
}
