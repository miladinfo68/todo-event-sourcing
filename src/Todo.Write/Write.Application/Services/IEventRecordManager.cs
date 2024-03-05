using Write.Domain.Entities;

namespace Write.Application.Services
{
    public interface IEventRecordManager
    {
        Task SaveEvent(TodoAggregate todoAggregate);
        TodoAggregate ProduceAggregate(Guid aggregateId);
    }
}
