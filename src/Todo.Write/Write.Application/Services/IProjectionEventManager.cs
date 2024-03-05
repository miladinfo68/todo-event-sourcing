using Core.Domain.Abstractions;

namespace Write.Application.Services
{
    public interface IProjectionEventManager
    {
        public IProjectionEvent GetEvent(IDomainEvent domainEvent);
    }
}
