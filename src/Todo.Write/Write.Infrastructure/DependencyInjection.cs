using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Write.Application.Services;
using Write.Infrastructure.Persistence;
using Write.Infrastructure.Services;

namespace Write.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            ArgumentNullException.ThrowIfNull(services, nameof(services));

            services.AddScoped<IEventRecordManager, EventRecordManager>();
            services.AddSingleton<IProjectionEventManager, ProjectionEventManager>();

            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            services.AddDbContext<WriteDbContext>(opt => opt.UseNpgsql(configuration.GetConnectionString("Database")));
            services.AddScoped<IDbContextHandler, DbContextHandler>();

            return services;
        }
    }
}
