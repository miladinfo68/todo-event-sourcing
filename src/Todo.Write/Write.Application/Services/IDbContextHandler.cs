using Core.Domain.Base;
using Microsoft.EntityFrameworkCore;

namespace Write.Application.Services
{
    public interface IDbContextHandler
    {
        public DbSet<T> Get<T>() where T : EntityBase;
        public Task Commit();
    }
}
