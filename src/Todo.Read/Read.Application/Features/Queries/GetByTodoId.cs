using Core.Application.Common;
using Core.Domain.Exceptions;
using MediatR;
using Read.Application.Services;

namespace Read.Application.Features.Queries
{
    public static class GetByTodoId
    {
        public class Query : IRequest<BaseResult>
        {
            public Guid TodoId { get; set; }
        }

        public class QueryHandler : IRequestHandler<Query, BaseResult>
        {
            private readonly ITodoItemService _todoItemService;

            public QueryHandler(ITodoItemService todoItemService)
            {
                _todoItemService = todoItemService;
            }

            public async Task<BaseResult> Handle(Query request, CancellationToken cancellationToken)
            {
                var todoItem = await _todoItemService.GetByAggregateId(request.TodoId);

                if (todoItem is null)
                    throw new BusinessException("Todo not found", "9999");

                return BaseResult<Result>.Success(new Result { 
                    Title = todoItem.Title,
                    Content = todoItem.Content,
                    CustomerId = todoItem.CustomerId,
                    Status = todoItem.Status,
                });
            }
        }

        public class Result
        {
            public string Title { get; set; }
            public string Content { get; set; }
            public int Status { get; set; }
            public Guid CustomerId { get; set; }
        }
    }
}
