using MediatR;
using Microsoft.AspNetCore.Mvc;
using WorkerManager.Application.Commands;
using WorkerManager.Application.Dto;
using WorkerManager.Application.Queries;

namespace WorkerManager.Api.Controllers
{
    [ApiController]
    [Route("api/worker")]
    public class WorkerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public WorkerController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetWorkerDto>>> GetAll()
        {
            return Ok(await _mediator.Send(new GetWorkers()));
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<GetWorkerDto>> Get([FromRoute] Guid id)
        {
            return Ok(await _mediator.Send(new GetWorker(id)));
        }
        [HttpPost("{id:Guid}/completetask")]
        public async Task<IActionResult> CompleteTask([FromRoute] Guid id)
        {
           return Ok(await _mediator.Send(new MarkTaskAsCompleted(id)));
        }

    }
}
