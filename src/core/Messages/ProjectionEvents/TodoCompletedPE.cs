using Core.Domain.Base;

namespace Messages.ProjectionEvents
{
    public class TodoCompletedPE : ProjectionEventBase
    {
        public Guid AggregateId { get; set; }
        public Guid CustomerId { get; set; }

        public TodoCompletedPE() : base(typeof(TodoCompletedPE).AssemblyQualifiedName)
        {

        }
    }
}
