using Microsoft.EntityFrameworkCore;
using Shared.Abstractions.Context;

namespace Shared.Infrastructure.Context
{
    internal sealed class ApplicationDbContext : DbContext, IApplicationDbContext
    {

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
