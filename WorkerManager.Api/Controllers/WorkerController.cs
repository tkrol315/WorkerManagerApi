using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkerManager.Application.Commands;
using WorkerManager.Application.Dto;
using WorkerManager.Application.Queries;

namespace WorkerManager.Api.Controllers
{
    [ApiController]
    [Authorize(Roles = "Worker,Manager")]
    [Route("api/workers")]
    public class WorkerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public WorkerController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// Gets all workers
        /// </summary>
        /// <returns>List of workers</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetWorkerDto>>> GetAll()
        {
            return Ok(await _mediator.Send(new GetWorkers()));
        }
        /// <summary>
        /// Gets a worker by worker ID
        /// </summary>
        /// <param name="id">Worker's ID</param>
        /// <returns>The requested worker</returns>
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<GetWorkerDto>> Get([FromRoute] Guid id)
        {
            return Ok(await _mediator.Send(new GetWorker(id)));
        }
        /// <summary>
        /// Marks a task as completed and enables the manager to assign a new task to this worker
        /// </summary>
        /// <param name="id">Worker's ID</param>
        /// <returns>Ok if the task is marked as completed</returns>
        [HttpPost("{id:Guid}/completedtask")]
        public async Task<IActionResult> CompleteTask([FromRoute] Guid id)
        {
           return Ok(await _mediator.Send(new MarkTaskAsCompleted(id)));
        }

    }
}
