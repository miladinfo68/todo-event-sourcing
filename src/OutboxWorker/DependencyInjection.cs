using Core.Infrastructure.Dependencies;
using Core.Infrastructure.DependencyOptions;

namespace OutboxWorker
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDependencies(this IServiceCollection services, 
            IConfiguration configuration)
        {
            ArgumentNullException.ThrowIfNull(services, nameof(services));

            services.AddConnectionFactoryDependency(opt => {
                opt.ConnectionString = configuration.GetConnectionString("WriteDatabase");
            });

            services.AddBrokerDependency(options => {
                options.ServiceType = ServiceType.Producer;
                options.BrokerAddress = configuration.GetValue<string>("Kafka:BootstrapServers");
            });

            return services;
        }
    }
}
