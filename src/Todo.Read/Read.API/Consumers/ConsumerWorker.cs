using Core.Application.Services;
using Core.Domain.Abstractions;
using Core.Domain.Base;
using MediatR;
using Messages;
using Messages.ProjectionEvents;
using Read.Application.Features.Commands;
using System.Text.Json;

namespace Read.API.Consumers
{
    public class ConsumerWorker : BackgroundService
    {
        private readonly IEventConsumer _consumer;
        private readonly IServiceProvider _serviceProvider;

        public ConsumerWorker(IEventConsumer consumer, IServiceProvider serviceProvider)
        {
            _consumer = consumer;
            _serviceProvider = serviceProvider;
        }

        // AutoMapper
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await this._consumer.ConsumeEvent(KafkaConsts.ReadTopicName, async (message) =>
            {
                var baseEvent = JsonSerializer.Deserialize<EventBase>(message);

                dynamic command = null;

                if (baseEvent.Type == typeof(TodoCreatedPE).AssemblyQualifiedName)
                {
                    var todoCreated = JsonSerializer.Deserialize<TodoCreatedPE>(message);
                    command = new CreateTodo.Command
                    {
                        AggregateId = todoCreated.AggregateId,
                        Content = todoCreated.Content,
                        CustomerId = todoCreated.CustomerId,
                        Title = todoCreated.Title
                    };
                }

                if (baseEvent.Type == typeof(ContentChangedPE).AssemblyQualifiedName)
                {
                    var contentChanged = JsonSerializer.Deserialize<ContentChangedPE>(message);
                    command = new ChangeContent.Command
                    {
                        AggregateId = contentChanged.AggregateId,
                        Content = contentChanged.Content,
                        CustomerId = contentChanged.CustomerId
                    };
                }

                if (baseEvent.Type == typeof(TitleChangedPE).AssemblyQualifiedName)
                {
                    var titleChanged = JsonSerializer.Deserialize<TitleChangedPE>(message);
                    command = new ChangeTitle.Command
                    {
                        AggregateId = titleChanged.AggregateId,
                        Title = titleChanged.Title,
                        CustomerId = titleChanged.CustomerId
                    };
                }

                if (baseEvent.Type == typeof(TodoCompletedPE).AssemblyQualifiedName)
                {
                    var todoCompleted = JsonSerializer.Deserialize<TodoCompletedPE>(message);
                    command = new CompleteTodo.Command
                    {
                        AggregateId = todoCompleted.AggregateId,
                        CustomerId = todoCompleted.CustomerId
                    };
                }

                if (baseEvent.Type == typeof(TodoStatusChangedPE).AssemblyQualifiedName)
                {
                    var todoStatusChanged = JsonSerializer.Deserialize<TodoStatusChangedPE>(message);
                    command = new ChangeTodoStatus.Command
                    {
                        AggregateId = todoStatusChanged.AggregateId,
                        CustomerId = todoStatusChanged.CustomerId,
                        Status = todoStatusChanged.Status
                    };
                }

                using (var scope = this._serviceProvider.CreateScope())
                {
                    var mediator = scope.ServiceProvider.GetService<IMediator>();

                    await mediator.Send(command);

                }

            }, stoppingToken);
        }
    }

    public class EventBase
    {
        public string Type { get; set; }
    }
}
