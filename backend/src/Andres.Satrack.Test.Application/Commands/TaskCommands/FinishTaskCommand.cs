using Andres.Satrack.Test.Domain.Aggregates.TaskAggregate;
using Microsoft.Extensions.Logging;
using SharedKernel.Application.Cqrs.Commands;
using SharedKernel.Application.Cqrs.Commands.Handlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Andres.Satrack.Test.Application.Commands.TaskCommands
{
    public record FinishTaskCommand(Guid TaskId) : IRequest<bool>;

    public class FinishTaskCommandHandler : IRequestHandler<FinishTaskCommand, bool>
    {
        private readonly ITaskRepository taskRepository;
        private readonly ILogger<FinishTaskCommandHandler> logger;

        public FinishTaskCommandHandler(ITaskRepository taskRepository, ILogger<FinishTaskCommandHandler> logger)
        {
            this.taskRepository = taskRepository;
            this.logger = logger;
        }

        public async Task<bool> HandleAsync(FinishTaskCommand command, CancellationToken cancellationToken)
        {
            var task = await taskRepository.GetByIdAsync(command.TaskId, cancellationToken);

            task.Finish();

            logger.LogInformation("Finishing task ({@Task})", task.Id);

            taskRepository.Update(task);

            int changes = await taskRepository.SaveChangesAsync(cancellationToken);

            return changes > 0;
        }
    }
}
