using Core.Domain.Base;
using Microsoft.EntityFrameworkCore;
using Write.Application.Services;
using Write.Infrastructure.Persistence;

namespace Write.Infrastructure.Services
{
    public class DbContextHandler : IDbContextHandler
    {
        private readonly WriteDbContext _dbContext;

        public DbContextHandler(WriteDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Commit()
        {
            await _dbContext.SaveChangesAsync();
        }

        public DbSet<T> Get<T>() where T : EntityBase
        {
            return _dbContext.Set<T>();
        }
    }
}
