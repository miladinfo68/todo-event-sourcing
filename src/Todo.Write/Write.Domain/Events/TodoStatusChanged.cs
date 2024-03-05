using Core.Domain.Abstractions;
using Write.Domain.Entities;

namespace Write.Domain.Events
{
    public class TodoStatusChanged : IDomainEvent
    {
        public Guid AggregateId { get; set; }
        public Guid CustomerId { get; set; }
        public int Status { get; set; }
    }
}
