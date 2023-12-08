using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WorkerManager.Application.Dto;
using WorkerManager.Application.Queries;

namespace WorkerManager.Api.Controllers
{
    [ApiController]
    [Authorize(Roles = "Manager")]
    [Route("api/managers")]
    public class ManagerController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ManagerController(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// Gets a manager by ID with associated tasks
        /// </summary>
        /// <param name="id">Manager's ID</param>
        /// <returns>The manager with tasks</returns>
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<GetManagerDto>> Get([FromRoute] Guid id)
        {
            return Ok(await _mediator.Send(new GetManager(id)));
        }
        /// <summary>
        /// Gets all managers with associated tasks
        /// </summary>
        /// <returns>List of managers with tasks</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetManagerDto>>> GetAll()
        {
            return Ok(await _mediator.Send(new GetManagers()));
        }

       
    }
}
