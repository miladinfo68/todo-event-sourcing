using Core.Domain.Abstractions;

namespace Write.Domain.Events
{
    public class ContentChanged : IDomainEvent
    {
        public Guid AggregateId { get; set; }
        public Guid CustomerId { get; set; }
        public string Content { get; set; }
    }
}
