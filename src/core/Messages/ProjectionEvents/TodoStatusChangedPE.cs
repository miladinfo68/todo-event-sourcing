using Core.Domain.Base;

namespace Messages.ProjectionEvents
{
    public class TodoStatusChangedPE : ProjectionEventBase
    {
        public Guid AggregateId { get; set; }
        public Guid CustomerId { get; set; }
        public int Status { get; set; }

        public TodoStatusChangedPE() : base(typeof(TodoStatusChangedPE).AssemblyQualifiedName)
        {

        }
    }
}
