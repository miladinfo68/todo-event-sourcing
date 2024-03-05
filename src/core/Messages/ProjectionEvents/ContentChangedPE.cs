using Core.Domain.Base;

namespace Messages.ProjectionEvents
{
    public class ContentChangedPE : ProjectionEventBase
    {
        public Guid AggregateId { get; set; }
        public Guid CustomerId { get; set; }
        public string Content { get; set; }

        public ContentChangedPE() : base(typeof(ContentChangedPE).AssemblyQualifiedName)
        {

        }
    }
}
