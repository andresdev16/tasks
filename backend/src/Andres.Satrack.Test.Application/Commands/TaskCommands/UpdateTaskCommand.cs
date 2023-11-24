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
    public record UpdateTaskCommand(Guid TaskId, string Name, string Category, DateTime LimitDate) : IRequest<bool>;

    public class UpdateTaskCommandHandler : IRequestHandler<UpdateTaskCommand, bool>
    {
        private readonly ITaskRepository taskRepository;
        private readonly ILogger<UpdateTaskCommandHandler> logger;

        public UpdateTaskCommandHandler(ITaskRepository taskRepository, ILogger<UpdateTaskCommandHandler> logger)
        {
            this.taskRepository = taskRepository;
            this.logger = logger;
        }

        public async Task<bool> HandleAsync(UpdateTaskCommand command, CancellationToken cancellationToken)
        {
            var task = await taskRepository.GetByIdAsync(command.TaskId, cancellationToken);

            task.Edit(command.Name, command.Category, command.LimitDate);

            logger.LogInformation("Editing task ({@Task})", task.Id);

            taskRepository.Update(task);

            int changes = await taskRepository.SaveChangesAsync(cancellationToken);

            return changes > 0;
        }
    }
}
