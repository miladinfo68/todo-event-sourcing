using Core.Domain.Abstractions;

namespace Write.Domain.Events
{
    public class TodoCompleted : IDomainEvent
    {
        public Guid AggregateId { get; set; }
        public Guid CustomerId { get; set; }
    }
}
