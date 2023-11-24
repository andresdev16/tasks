using Andres.Satrack.Test.Domain.Aggregates.TaskAggregate;
using Microsoft.Extensions.Logging;
using SharedKernel.Application.Cqrs.Commands;
using SharedKernel.Application.Cqrs.Commands.Handlers;

namespace Andres.Satrack.Test.Application.Commands.TaskCommands
{
    public record AddTaskCommand(string Name, string Category, DateTime LimitDate) : IRequest<Guid>;

    public class AddTaskCommandHandler : IRequestHandler<AddTaskCommand, Guid>
    {
        private readonly ITaskRepository taskRepository;
        private readonly ILogger<AddTaskCommandHandler> logger;

        public AddTaskCommandHandler(ITaskRepository taskRepository, ILogger<AddTaskCommandHandler> logger)
        {
            this.taskRepository = taskRepository;
            this.logger = logger;
        }

        public async Task<Guid> HandleAsync(AddTaskCommand command, CancellationToken cancellationToken)
        {
            var task = Domain.Aggregates.TaskAggregate.Task.Create(command.Name, command.Category, command.LimitDate);

            taskRepository.Add(task);

            logger.LogInformation("Saving new Task ({@Task})", task.Name);

            await taskRepository.SaveChangesAsync(cancellationToken);

            return task.Id;

        }
    }
}
