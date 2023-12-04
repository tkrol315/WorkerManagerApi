using MediatR;
using Microsoft.AspNetCore.Mvc;
using WorkerManager.Application.Commands;
using WorkerManager.Application.Dto;
using WorkerManager.Application.Queries;

namespace WorkerManager.Api.Controllers
{
    [ApiController]
    [Route("api/manager")]
    public class ManagerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ManagerController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<GetManagerDto>> Get([FromRoute] Guid id)
        {
           var query = new GetManager(id);
            return Ok(await _mediator.Send(query));
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetManagerDto>>> GetAll()
        {
            return Ok(await _mediator.Send(new GetManagers()));
        }

        [HttpGet("{id:guid}/task")]
        public async Task<ActionResult<IEnumerable<GetTaskManagerDto>>> GetAll([FromRoute] Guid id)
        {
            return Ok(await _mediator.Send(new GetManagersTasks(id)));
        }
        [HttpGet("{id:guid}/task/{taskName}")]
        public async Task<ActionResult<GetTaskManagerDto>> Get([FromRoute] Guid id, [FromRoute] string taskName)
        {
            var query = new GetManagersTask(id, taskName);
            return Ok(await _mediator.Send(query));
        }
        [HttpPost("{id:guid}/task")]
        public async Task<IActionResult> CreateTask([FromRoute] Guid id, [FromBody] CreateTaskDto taskDto)
        {
            var command = new CreateTask(id, taskDto);
            var task = await _mediator.Send(command);
            return Created($"api/account/manager/{id}/task/{taskDto.Name}", null);
        }
        [HttpPost("{id:guid}/task/{taskName}/assign")]
        public async Task<IActionResult> AssignTask([FromRoute] Guid id, [FromRoute] string taskName,
            [FromQuery] Guid workerId)
        {
            var command = new AssignTask(id, workerId, taskName);
            await _mediator.Send(command);
            return Ok();
        }
    }
}
