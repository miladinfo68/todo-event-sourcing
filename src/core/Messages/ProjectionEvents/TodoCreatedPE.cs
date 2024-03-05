using Core.Domain.Base;

namespace Messages.ProjectionEvents
{
    public class TodoCreatedPE : ProjectionEventBase
    {
        public Guid AggregateId { get; set; }
        public Guid CustomerId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }

        public TodoCreatedPE() : base(typeof(TodoCreatedPE).AssemblyQualifiedName)
        {

        }
    }
}
