using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkerManager.Application.Commands;
using WorkerManager.Application.Dto;
using WorkerManager.Application.Queries;

namespace WorkerManager.Api.Controllers
{
    [ApiController]
    [Authorize(Roles = "Manager")]
    [Route("api/managers/{id:guid}/tasks")]
    public class TaskController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TaskController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Gets manager tasks
        /// </summary>
        /// <param name="id">Manager's ID</param>
        /// <returns>List of manager tasks</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetTaskManagerDto>>> GetAll([FromRoute] Guid id)
        {
            return Ok(await _mediator.Send(new GetManagersTasks(id)));
        }
        /// <summary>
        /// Gets a manager task by taskName
        /// </summary>
        /// <param name="id">Manager's ID</param>
        /// <param name="taskName">Task name</param>
        /// <returns>The requested manager task</returns>
        [HttpGet("{taskName}")]
        public async Task<ActionResult<GetTaskManagerDto>> Get([FromRoute] Guid id, [FromRoute] string taskName)
        {
            return Ok(await _mediator.Send(new GetManagersTask(id, taskName)));
        }
        /// <summary>
        /// Creates and adds a task to the manager's task list
        /// </summary>
        /// <param name="id">Manager's ID</param>
        /// <param name="taskDto">Task Dto</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Create([FromRoute] Guid id, [FromBody] CreateTaskDto taskDto)
        {
            var task = await _mediator.Send(new CreateTask(id, taskDto));
            return Created($"api/managers/{id}/tasks/{taskDto.Name}", null);
        }
        /// <summary>
        /// Assigns a task to a worker.
        /// </summary>
        /// <param name="id">Manager's ID</param>
        /// <param name="taskName">Task name</param>
        /// <param name="workerId">Worker's ID</param>
        /// <returns></returns>
        [HttpPost("{taskName}/assign")]
        public async Task<IActionResult> Assign([FromRoute] Guid id, [FromRoute] string taskName,
            [FromQuery] Guid workerId)
        {
            return Ok(await _mediator.Send(new AssignTask(id, workerId, taskName)));
        }
        /// <summary>
        /// Removes a task from the manager's list
        /// </summary>
        /// <param name="id">Manager's ID</param>
        /// <param name="taskName">Task name</param>
        /// <returns></returns>
        [HttpDelete("{taskName}")]
        public async Task<IActionResult> Remove([FromRoute] Guid id, [FromRoute] string taskName)
        {
            await _mediator.Send(new RemoveTask(id, taskName));
            return NoContent();
        }
        /// <summary>
        /// Updates a task in the manager's list
        /// </summary>
        /// <param name="id">Manager's ID</param>
        /// <param name="taskName">Task name</param>
        /// <param name="dto">Updated task data</param>
        /// <returns></returns>
        [HttpPut("{taskName}")]
        public async Task<IActionResult> Update([FromRoute] Guid id, [FromRoute] string taskName, [FromBody] UpdateTaskDto dto)
        {
            return Ok(await _mediator.Send(new UpdateTask(id, taskName, dto)));
        }
    }
}
