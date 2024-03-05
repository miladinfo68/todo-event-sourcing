using MediatR;
using Read.Application.Services;

namespace Read.Application.Features.Commands
{
    public static class CreateTodo
    {
        public class Command : IRequest
        {
            public Guid AggregateId { get; set; }
            public Guid CustomerId { get; set; }
            public string Title { get; set; }
            public string Content { get; set; }
        }

        public class CommandHandler : IRequestHandler<Command>
        {
            private readonly ITodoItemService _todoItemService;

            public CommandHandler(ITodoItemService todoItemService)
            {
                _todoItemService = todoItemService;
            }

            public async Task Handle(Command request, CancellationToken cancellationToken)
            {
                await _todoItemService.Create(new Domain.Models.TodoItem
                {
                    AggregateId = request.AggregateId,
                    CustomerId = request.CustomerId,
                    Title = request.Title,
                    Content = request.Content,
                    Status = 0
                });
            }
        }
    }
}
