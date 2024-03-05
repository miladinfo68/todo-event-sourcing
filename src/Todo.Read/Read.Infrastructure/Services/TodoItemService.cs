using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Read.Application.Options;
using Read.Application.Services;
using Read.Domain.Models;

namespace Read.Infrastructure.Services
{
    public class TodoItemService : ITodoItemService
    {
        private readonly DatabaseOptions _dbOptions;
        private readonly IMongoCollection<TodoItem> _todoItems;
        public TodoItemService(IOptions<DatabaseOptions> databaseOptions, 
            IMongoClient mongoClient)
        {
            _dbOptions = databaseOptions.Value;

            var database = mongoClient.GetDatabase(_dbOptions.DatabaseName);
            _todoItems = database.GetCollection<TodoItem>(_dbOptions.CollectionName);
        }

        public async Task<TodoItem> Create(TodoItem todoItem)
        {
            await _todoItems.InsertOneAsync(todoItem);

            return todoItem;
        }

        public async Task<List<TodoItem>> Get()
        {
            return (await _todoItems.FindAsync(t => true)).ToList();
        }

        public async Task<TodoItem> Get(string id)
        {
            return (await _todoItems.FindAsync(x => x.Id == id)).FirstOrDefault();
        }

        public async Task<TodoItem> GetByAggregateId(Guid aggregateId)
        {
            return (await _todoItems.FindAsync(x => x.AggregateId == aggregateId)).FirstOrDefault();
        }

        public async Task Remove(string id)
        {
            await _todoItems.DeleteOneAsync(x => x.Id == id);
        }

        public async Task Update(TodoItem todoItem)
        {
            await _todoItems.ReplaceOneAsync(x => x.Id == todoItem.Id, todoItem);
        }
    }
}
