using Core.Domain.Abstractions;
using Core.Domain.Base;
using Core.Domain.Exceptions;
using Messages;
using System.Text.Json;
using Write.Application.Services;
using Write.Domain.Entities;

namespace Write.Infrastructure.Services
{
    public class EventRecordManager : IEventRecordManager
    {
        private readonly IDbContextHandler _dbContextHandler;
        private readonly IProjectionEventManager _projectionEventManager;

        public EventRecordManager(IDbContextHandler dbContextHandler,
            IProjectionEventManager projectionEventManager)
        {
            _dbContextHandler = dbContextHandler;
            _projectionEventManager = projectionEventManager;
        }

        public TodoAggregate ProduceAggregate(Guid aggregateId)
        {
            var todoAggregate = TodoAggregate.Produce();

            var eventRecords = this._dbContextHandler.Get<EventRecord>()
                .Where(x => x.AggregateId == aggregateId).OrderBy(x => x.CreatedOn).ToList();

            if (eventRecords is null || eventRecords.Count() == 0)
                throw new BusinessException("Todo not found", "9999");

            foreach (var @event in eventRecords)
            {
                var domainEvent = (IDomainEvent)JsonSerializer.Deserialize(@event.Payload, Type.GetType(@event.EventType));

                todoAggregate.AddDomainEvent(domainEvent);
            }

            todoAggregate.ClearDomainEvents();

            return todoAggregate;
        }

        public async Task SaveEvent(TodoAggregate todoAggregate)
        {
            foreach (var domainEvent in todoAggregate.DomainEvents)
            {
                await this._dbContextHandler.Get<EventRecord>()
                    .AddAsync(EventRecord.Create(todoAggregate.AggregateId, domainEvent.GetType().AssemblyQualifiedName,
                    JsonSerializer.Serialize(domainEvent, domainEvent.GetType())));

                var integrationEvent = _projectionEventManager.GetEvent(domainEvent);

                await this._dbContextHandler.Get<OutboxMessage>()
                    .AddAsync(OutboxMessage.Create(integrationEvent.GetType().AssemblyQualifiedName, JsonSerializer.Serialize(integrationEvent, integrationEvent.GetType()),
                    KafkaConsts.ReadTopicName));
            }
        }
    }
}
