using Core.Application.Common;
using MediatR;
using Write.Application.Services;
using Write.Domain.Entities;

namespace Write.Application.Features
{
    public static class AddTodo
    {
        public class Command : IRequest<BaseResult>
        {
            public string Title { get; set; }
            public string Content { get; set; }
            public Guid CustomerId { get; set; }
        }

        public class CommandHandler : IRequestHandler<Command, BaseResult>
        {
            private readonly IEventRecordManager _eventRecordManager;
            private readonly IDbContextHandler _dbContextHandler;

            public CommandHandler(IEventRecordManager eventRecordManager,
                IDbContextHandler dbContextHandler)
            {
                _eventRecordManager = eventRecordManager;
                _dbContextHandler = dbContextHandler;
            }

            public async Task<BaseResult> Handle(Command request, CancellationToken cancellationToken)
            {
                var todoItem = TodoAggregate.Create(request.CustomerId, request.Title, request.Content);

                await _eventRecordManager.SaveEvent(todoItem);

                await _dbContextHandler.Commit();

                return BaseResult<Response>.Success(new Response { 
                    TodoId = todoItem.AggregateId
                });
            }
        }

        public class Response
        {
            public Guid TodoId { get; set; }
        }
    }
}
