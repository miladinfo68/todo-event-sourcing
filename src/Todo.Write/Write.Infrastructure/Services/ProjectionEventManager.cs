using Core.Domain.Abstractions;
using Messages.ProjectionEvents;
using Write.Application.Services;
using Write.Domain.Events;

namespace Write.Infrastructure.Services
{
    public class ProjectionEventManager : IProjectionEventManager
    {
        public IProjectionEvent GetEvent(IDomainEvent domainEvent)
        {
            // Automapper
            if (domainEvent is TodoCreated tc)
                return new TodoCreatedPE() {
                    AggregateId = tc.AggregateId,
                    Content = tc.Content,
                    CustomerId = tc.CustomerId,
                    Title = tc.Title,
                };

            if (domainEvent is TitleChanged tch)
                return new TitleChangedPE()
                {
                    AggregateId = tch.AggregateId,
                    CustomerId = tch.CustomerId,
                    Title = tch.Title
                };

            if (domainEvent is ContentChanged ch)
                return new ContentChangedPE()
                {
                    AggregateId = ch.AggregateId,
                    CustomerId = ch.CustomerId,
                    Content = ch.Content
                };

            if (domainEvent is TodoCompleted completed)
                return new TodoCompletedPE()
                {
                    AggregateId = completed.AggregateId,
                    CustomerId = completed.CustomerId,
                };

            if (domainEvent is TodoStatusChanged statusChanged)
                return new TodoStatusChangedPE()
                {
                    AggregateId = statusChanged.AggregateId,
                    CustomerId = statusChanged.CustomerId,
                    Status = statusChanged.Status
                };

            return default(IProjectionEvent);
        }
    }
}
