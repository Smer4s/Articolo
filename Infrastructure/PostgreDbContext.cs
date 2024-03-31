using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Infrastructure
{
    public class PostgreDbContext(DbContextOptions<PostgreDbContext> dbContextOptions, IOptions<DatabaseConfiguration> databaseConfig) : DbContext(dbContextOptions)
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(databaseConfig.Value.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(databaseConfig.Value.DefaultScheme);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PostgreDbContext).Assembly);
        }
    }
}
