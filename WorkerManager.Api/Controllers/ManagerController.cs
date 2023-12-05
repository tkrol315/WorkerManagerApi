using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;
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
            return Ok(await _mediator.Send(new GetManager(id)));
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetManagerDto>>> GetAll()
        {
            return Ok(await _mediator.Send(new GetManagers()));
        }

       
    }
}
