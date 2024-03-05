using Core.Domain.Abstractions;
using Core.Domain.Base;

namespace Write.Domain.Entities
{
    public class EventRecord : EntityBase, IAggregateRoot
    {
        public Guid AggregateId { get; private set; }
        public string EventType { get; private set; }
        public string Payload { get; private set; }

        protected EventRecord()
        {

        }

        private EventRecord(Guid aggregateId, string eventType, string payload)
        {
            this.AggregateId = aggregateId;
            this.EventType = eventType;
            this.Payload = payload;
        }
        
        public static EventRecord Create(Guid aggregateId, string eventType, string payload)
        {
            return new EventRecord(aggregateId, eventType, payload);
        }
    }
}
