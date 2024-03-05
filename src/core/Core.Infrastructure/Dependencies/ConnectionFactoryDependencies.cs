using Core.Application.Services;
using Core.Infrastructure.DependencyOptions;
using Core.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Infrastructure.Dependencies
{
    public static class ConnectionFactoryDependencies
    {
        public static IServiceCollection AddConnectionFactoryDependency(this IServiceCollection services, Action<ConnectionFactoryDependencyOptions> options)
        {
            var dependencyOptions = new ConnectionFactoryDependencyOptions();
            options(dependencyOptions);

            services.AddSingleton<IDbConnectionFactory>(x => new PostgreDbConnectionFactory(dependencyOptions.ConnectionString));

            return services;
        }
    }
}
