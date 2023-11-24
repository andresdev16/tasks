using Andres.Satrack.Test.Application.Commands.TaskCommands;
using Andres.Satrack.Test.Application.Queries;
using Microsoft.AspNetCore.Mvc;
using SharedKernel.Application.Cqrs.Commands;
using SharedKernel.Application.Cqrs.Queries;
using System.Net;
using System.Net.Mime;

namespace Andres.Satrack.Test.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : Controller
    {
        private readonly ISender sender;
        private readonly IQueryBus queries;

        public TaskController(ISender sender, IQueryBus queries)
        {
            this.sender = sender;
            this.queries = queries;
        }

        [HttpGet("all:{offset:int}:{limit:int}")]
        [ActionName(nameof(GetSummaryAsync))]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetSummaryAsync(int offset, int limit, CancellationToken cancellationToken)
        {
            var summary = await queries.Ask(new GetTaskSummaryQuery(limit, offset), cancellationToken);

            return Ok(summary);
        }

        [HttpPost()]
        [ActionName(nameof(CreateTaskAsync))]
        [Consumes(MediaTypeNames.Application.Json)]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateTaskAsync(AddTaskCommand command, CancellationToken cancellationToken)
        {
            Guid id = await sender.SendAsync(command, cancellationToken);

            return Created("", id);
        }

        [HttpPut()]
        [ActionName(nameof(UpdateTaskAsync))]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> UpdateTaskAsync(UpdateTaskCommand command, CancellationToken cancellationToken)
        {
            bool success = await sender.SendAsync(command, cancellationToken);

            return success
                ? NoContent()
                : BadRequest();
        }

        [HttpDelete()]
        [ActionName(nameof(DeleteTaskAsync))]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> DeleteTaskAsync(DeleteTaskCommand command, CancellationToken cancellationToken)
        {
            bool success = await sender.SendAsync(command, cancellationToken);

            return success
                ? NoContent()
                : BadRequest();
        }
    }
}
