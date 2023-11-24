using Andres.Satrack.Test.Domain.Aggregates.TaskAggregate;
using SharedKernel.Infrastructure.Data.EntityFrameworkCore.DbContexts;
using SharedKernel.Infrastructure.Data.EntityFrameworkCore.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andres.Satrack.Test.Infrastructure.Persistence.Repositories
{
    internal class TaskRepository : EntityFrameworkCoreRepositoryAsync<Domain.Aggregates.TaskAggregate.Task>, ITaskRepository
    {
        private readonly TaskContext context;

        public TaskRepository(TaskContext context) : base(context)
        {
            this.context = context;
        }
    }
}