using Andres.Satrack.Test.Application.Commands.TaskCommands;
using Andres.Satrack.Test.Domain.Aggregates.TaskAggregate;
using Andres.Satrack.Test.Infrastructure.Persistence;
using Andres.Satrack.Test.Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel.Infrastructure;
using SharedKernel.Infrastructure.Cqrs.Commands;
using SharedKernel.Infrastructure.Cqrs.Queries;
using SharedKernel.Infrastructure.Data.Dapper;
using SharedKernel.Infrastructure.Data.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andres.Satrack.Test.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCore(this IServiceCollection services, IConfiguration configuration, string connectionString)
        {
            return services
                .AddSharedKernel()
                .AddInfrastructure(configuration, connectionString)
                .AddApplication();
        }

        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration, string connectionString)
        {
            return services
                .AddDapperSqlServer(configuration, connectionString)
                .AddEntityFrameworkCoreSqlServer<TaskContext>(configuration, connectionString)
                .AddTransient<ITaskRepository, TaskRepository>();
        }

        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            return services
                .AddCommandsHandlers(typeof(AddTaskCommandHandler))
                .AddQueriesHandlers(typeof(TaskContext));
        }
    }
}
