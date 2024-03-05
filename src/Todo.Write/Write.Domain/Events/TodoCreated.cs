using Core.Domain.Abstractions;

namespace Write.Domain.Events
{
    public class TodoCreated : IDomainEvent
    {
        public Guid AggregateId { get; set; }
        public Guid CustomerId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
