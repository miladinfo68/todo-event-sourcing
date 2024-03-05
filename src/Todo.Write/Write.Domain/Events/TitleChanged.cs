using Core.Domain.Abstractions;

namespace Write.Domain.Events
{
    public class TitleChanged : IDomainEvent
    {
        public Guid AggregateId { get; set; }
        public Guid CustomerId { get; set; }
        public string Title { get; set; }
    }
}
