using Core.Domain.Abstractions;
using Core.Domain.Base;
using Write.Domain.Events;

namespace Write.Domain.Entities
{
    public class TodoAggregate : AggregateRootBase
    {
        public Guid CustomerId { get; private set; }
        public string Title { get; private set; }
        public string Content { get; private set; }
        public TodoStatus Status { get; private set; }

        private TodoAggregate()
        {
            
        }

        private TodoAggregate(Guid customerId, string title, string content)
        {
            AddDomainEvent(new TodoCreated { 
                AggregateId = Guid.NewGuid(),
                CustomerId = customerId,
                Title = title,
                Content = content
            });
        }

        public static TodoAggregate Create(Guid customerId, string title, string content)
        {
            return new TodoAggregate(customerId, title, content);
        }

        // Use with Aggregate Producer
        public static TodoAggregate Produce()
        {
            return new TodoAggregate();
        }

        public override void AddDomainEvent(IDomainEvent domainEvent)
        {
            base.AddDomainEvent(domainEvent);

            if (domainEvent is TodoCreated todoCreated)
            {
                this.AggregateId = todoCreated.AggregateId;
                this.CustomerId = todoCreated.CustomerId;
                this.Title = todoCreated.Title;
                this.Content = todoCreated.Content;
            }

            if (domainEvent is TitleChanged titleChanged)
                this.Title = titleChanged.Title;

            if (domainEvent is ContentChanged contentChanged)
                this.Content = contentChanged.Content;

            if (domainEvent is TodoCompleted)
                this.Status = TodoStatus.Completed;

            if (domainEvent is TodoStatusChanged statusChanged)
                this.Status = (TodoStatus)statusChanged.Status;
        }

        public void ChangeTitle(string newTitle)
        {
            AddDomainEvent(new TitleChanged() { 
                AggregateId = this.AggregateId,
                CustomerId = this.CustomerId,
                Title = newTitle
            });
        }

        public void ChangeContent(string newContent)
        {
            AddDomainEvent(new ContentChanged() { 
                AggregateId = this.AggregateId,
                CustomerId = this.CustomerId,
                Content = newContent
            });
        }

        public void ChangeStatus(TodoStatus newStatus)
        {
            AddDomainEvent(new TodoStatusChanged() { 
                AggregateId = this.AggregateId,
                CustomerId = this.CustomerId,
                Status = newStatus.GetHashCode()
            });
        }

        public void Completed()
        {
            AddDomainEvent(new TodoCompleted() { 
                AggregateId = this.AggregateId,
                CustomerId = this.CustomerId,
            });
        }
    }
}
