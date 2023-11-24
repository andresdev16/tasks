using SharedKernel.Application.Cqrs.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andres.Satrack.Test.Application.Queries
{
    public record GetTaskSummaryQuery(int Limit, int Offset) : IQueryRequest<IEnumerable<Task>>;
}
