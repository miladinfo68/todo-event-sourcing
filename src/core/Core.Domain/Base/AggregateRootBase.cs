using Core.Domain.Abstractions;
using System.Text.Json.Serialization;

namespace Core.Domain.Base
{
    public abstract class AggregateRootBase : EntityBase, IAggregateRoot
    {
        public Guid AggregateId { get; protected set; }

        [JsonIgnore]
        private List<IDomainEvent> _domainEvents;

        public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        public virtual void AddDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents ??= new();

            _domainEvents.Add(domainEvent);
        }

        public void RemoveDomainEvent(IDomainEvent domainEvent)
        {
            _domainEvents?.Remove(domainEvent);
        }

        public void ClearDomainEvents()
        {
            _domainEvents?.Clear();
        }
    }
}
