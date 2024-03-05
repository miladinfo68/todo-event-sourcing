using Read.Domain.Models;

namespace Read.Application.Services
{
    public interface ITodoItemService
    {
        Task<List<TodoItem>> Get();
        Task<TodoItem> Get(string id);
        Task<TodoItem> GetByAggregateId(Guid aggregateId);
        Task<TodoItem> Create(TodoItem todoItem);
        Task Update(TodoItem todoItem);
        Task Remove(string id);
    }
}
