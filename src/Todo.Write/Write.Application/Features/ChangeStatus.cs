using Core.Application.Common;
using Core.Domain.Exceptions;
using MediatR;
using Write.Application.Services;
using Write.Domain.Entities;

namespace Write.Application.Features
{
    public static class ChangeStatus
    {
        public class Command : IRequest<BaseResult>
        {
            public Guid TodoId { get; set; }
            public int StatusId { get; set; }
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
                var todo = _eventRecordManager.ProduceAggregate(request.TodoId);

                // Do that with FluentValidation
                if (!Enum.IsDefined(typeof(TodoStatus), request.StatusId))
                    throw new BusinessException("StatusId is not defined!", "9999");

                if (todo.Status.GetHashCode() == request.StatusId)
                    throw new BusinessException("Same status", "9999");

                todo.ChangeStatus((TodoStatus)request.StatusId);

                await _eventRecordManager.SaveEvent(todo);

                await _dbContextHandler.Commit();

                return BaseResult.Success();
            }
        }
    }
}
