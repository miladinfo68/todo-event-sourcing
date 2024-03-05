using Confluent.Kafka;
using Core.Application.Services;
using Core.Infrastructure.DependencyOptions;
using Core.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Infrastructure.Dependencies
{
    public static class BrokerDependencies
    {
        public static IServiceCollection AddBrokerDependency(this IServiceCollection services, Action<BrokerDependencyOptions> options)
        {
            var dependencyOptions = new BrokerDependencyOptions();
            options(dependencyOptions);

            if (dependencyOptions.ServiceType == ServiceType.Producer || dependencyOptions.ServiceType == ServiceType.Both)
            {
                services.AddSingleton<IProducer<Null, string>>(x => new ProducerBuilder<Null, string>(new ProducerConfig
                {
                    BootstrapServers = dependencyOptions.BrokerAddress,
                    Acks = Acks.Leader
                }).Build());

                services.AddSingleton<IEventProducer, EventProducer>();
            }

            if (dependencyOptions.ServiceType == ServiceType.Consumer || dependencyOptions.ServiceType == ServiceType.Both)
            {
                services.AddSingleton<IConsumer<Null, string>>(x => new ConsumerBuilder<Null, string>(new ConsumerConfig
                {
                    BootstrapServers = dependencyOptions.BrokerAddress,
                    AutoOffsetReset = AutoOffsetReset.Earliest,
                    GroupId = dependencyOptions.ConsumerGroupId,
                    AllowAutoCreateTopics = true
                }).Build());

                services.AddSingleton<IEventConsumer, EventConsumer>();
            }

            return services;
        }
    }
}
