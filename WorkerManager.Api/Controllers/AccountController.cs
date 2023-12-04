using MediatR;
using Microsoft.AspNetCore.Mvc;
using WorkerManager.Application.Commands;
using WorkerManager.Application.Queries;

namespace WorkerManager.Api.Controllers
{
    [ApiController]
    [Route("api/account")]
    public class AccountController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUser command)
        {
            await _mediator.Send(command);
            return Created($"api/account/register/{command.Dto.Id}", null);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Login query)
        {
            var token = await _mediator.Send(query);
            return Ok(token);
        }
    }
}
