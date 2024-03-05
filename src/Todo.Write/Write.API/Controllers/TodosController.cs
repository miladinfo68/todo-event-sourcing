using MediatR;
using Microsoft.AspNetCore.Mvc;
using Write.Application.Features;

namespace Write.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TodosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("AddTodo")]
        public async Task<IActionResult> AddTodo(AddTodo.Command command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPost("ChangeContent")]
        public async Task<IActionResult> ChangeContent(ChangeContent.Command command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPost("ChangeTitle")]
        public async Task<IActionResult> ChangeTitle(ChangeTitle.Command command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPost("ChangeStatus")]
        public async Task<IActionResult> ChangeStatus(ChangeStatus.Command command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPost("CompleteTodo")]
        public async Task<IActionResult> CompleteTodo(CompleteTodo.Command command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}
