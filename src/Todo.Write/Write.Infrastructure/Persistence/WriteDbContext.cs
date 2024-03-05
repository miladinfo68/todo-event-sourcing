using Core.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Write.Domain.Entities;

namespace Write.Infrastructure.Persistence
{
    public class WriteDbContext : BaseDbContext
    {
        public DbSet<EventRecord> EventRecords { get;set; }

        public WriteDbContext(DbContextOptions opt) : base(opt)
        {

        }
    }
}
