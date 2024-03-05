using Core.Application;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Read.Application.Options;
using System.Reflection;

namespace Read.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services,
            IConfiguration configuration)
        {
            ArgumentNullException.ThrowIfNull(services, nameof(services));

            services.AddCoreApplication(Assembly.GetExecutingAssembly());

            services.Configure<DatabaseOptions>(configuration.GetSection("DatabaseSettings"));

            return services;
        }
    }
}
