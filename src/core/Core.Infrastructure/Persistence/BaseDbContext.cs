using Core.Domain.Base;
using Microsoft.EntityFrameworkCore;

namespace Core.Infrastructure.Persistence
{
    public class BaseDbContext : DbContext
    {
        public BaseDbContext(DbContextOptions opt) : base(opt)
        {

        }

        public DbSet<OutboxMessage> OutboxMessages { get; set; }
    }
}
