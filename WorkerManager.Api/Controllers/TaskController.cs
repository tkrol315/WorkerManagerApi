using MediatR;
using Microsoft.AspNetCore.Mvc;
using WorkerManager.Application.Commands;
using WorkerManager.Application.Dto;
using WorkerManager.Application.Queries;

namespace WorkerManager.Api.Controllers
{
    [ApiController]
    [Route("api/manager/{id:guid}/task")]
    public class TaskController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TaskController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetTaskManagerDto>>> GetAll([FromRoute] Guid id)
        {
            return Ok(await _mediator.Send(new GetManagersTasks(id)));
        }
        [HttpGet("{taskName}")]
        public async Task<ActionResult<GetTaskManagerDto>> Get([FromRoute] Guid id, [FromRoute] string taskName)
        {
            return Ok(await _mediator.Send(new GetManagersTask(id, taskName)));
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromRoute] Guid id, [FromBody] CreateTaskDto taskDto)
        {
            var task = await _mediator.Send(new CreateTask(id, taskDto));
            return Created($"api/account/manager/{id}/task/{taskDto.Name}", null);
        }
        [HttpPost("{taskName}/assign")]
        public async Task<IActionResult> Assign([FromRoute] Guid id, [FromRoute] string taskName,
            [FromQuery] Guid workerId)
        {
            return Ok(await _mediator.Send(new AssignTask(id, workerId, taskName)));
        }
        [HttpDelete("{taskName}")]
        public async Task<IActionResult> Remove([FromRoute] Guid id, [FromRoute] string taskName)
        {
            await _mediator.Send(new RemoveTask(id, taskName));
            return NoContent();
        }
        [HttpPut("{taskName}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromRoute] string taskName, [FromBody] UpdateTaskDto dto)
        {
            return Ok(await _mediator.Send(new UpdateTask(id, taskName, dto)));
        }
    }
}
