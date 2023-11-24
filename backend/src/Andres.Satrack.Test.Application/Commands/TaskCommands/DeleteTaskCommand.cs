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
    public record DeleteTaskCommand(Guid TaskId) : IRequest<bool>;

    public class DeleteTaskCommandHandler : IRequestHandler<DeleteTaskCommand, bool>
    {
        private readonly ITaskRepository taskRepository;
        private readonly ILogger<DeleteTaskCommandHandler> logger;

        public DeleteTaskCommandHandler(ITaskRepository taskRepository, ILogger<DeleteTaskCommandHandler> logger)
        {
            this.taskRepository = taskRepository;
            this.logger = logger;
        }

        public async Task<bool> HandleAsync(DeleteTaskCommand command, CancellationToken cancellationToken)
        {
            var task = await taskRepository.GetByIdAsync(command.TaskId, cancellationToken);

            logger.LogInformation("Deleting Task ({@Task})", task.Id);

            taskRepository.Remove(task);

            int changes = await taskRepository.SaveChangesAsync(cancellationToken);

            return changes > 0;
        }
    }
}
