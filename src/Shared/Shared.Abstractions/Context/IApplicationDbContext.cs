using Microsoft.EntityFrameworkCore;

namespace Shared.Abstractions.Context
{
    public interface IApplicationDbContext
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;
        int SaveChanges();
    }
}
