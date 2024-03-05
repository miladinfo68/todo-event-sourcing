using Core.Domain.Base;

namespace Messages.ProjectionEvents
{
    public class TitleChangedPE : ProjectionEventBase
    {
        public Guid AggregateId { get; set; }
        public Guid CustomerId { get; set; }
        public string Title { get; set; }

        public TitleChangedPE() : base(typeof(TitleChangedPE).AssemblyQualifiedName)
        {

        }
    }
}
