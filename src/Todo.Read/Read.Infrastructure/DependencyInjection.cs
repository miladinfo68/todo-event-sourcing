using Core.Infrastructure.Dependencies;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using Read.Application.Services;
using Read.Infrastructure.Services;

namespace Read.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            ArgumentNullException.ThrowIfNull(services, nameof(services));

            services.AddSingleton<IMongoClient>(s => 
                new MongoClient(configuration.GetValue<string>("DatabaseSettings:ConnectionString")));

            services.AddBrokerDependency(opt => {
                opt.ConsumerGroupId = configuration.GetValue<string>("Kafka:ConsumerGroupId");
                opt.ServiceType = Core.Infrastructure.DependencyOptions.ServiceType.Consumer;
                opt.BrokerAddress = configuration.GetValue<string>("Kafka:BootstrapServers");
            });

            services.AddScoped<ITodoItemService, TodoItemService>();

            return services;
        }
    }
}
