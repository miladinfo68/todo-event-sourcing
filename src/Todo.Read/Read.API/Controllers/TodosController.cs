using MediatR;
using Microsoft.AspNetCore.Mvc;
using Read.Application.Features.Queries;

namespace Read.API.Controllers
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

        [HttpGet("FindById")]
        public async Task<ActionResult> FindById([FromQuery] GetByTodoId.Query query)
        {
            return Ok(await _mediator.Send(query));
        }
    }
}
