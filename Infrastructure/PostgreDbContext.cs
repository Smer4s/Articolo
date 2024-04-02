using Domain.Entities;
using Domain.Entities.Reactions;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Npgsql;

#pragma warning disable CS0618

namespace Infrastructure
{
    public class PostgreDbContext(DbContextOptions<PostgreDbContext> dbContextOptions, IOptions<DatabaseConfiguration> databaseConfig) : DbContext(dbContextOptions)
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<PublicationReaction> PublicationReactions { get; set; }
        public DbSet<CommentReaction> CommentReactions { get; set; }
        public DbSet<Theme> Themes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            NpgsqlConnection.GlobalTypeMapper.MapEnum<ReactionType>();
            NpgsqlConnection.GlobalTypeMapper.MapEnum<Status>();
            NpgsqlConnection.GlobalTypeMapper.MapEnum<Role>();

            optionsBuilder.UseNpgsql(databaseConfig.Value.ConnectionString);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            InitRelations(builder);

            InitEnums(builder);

            builder.HasDefaultSchema(databaseConfig.Value.DefaultScheme);

            builder.ApplyConfigurationsFromAssembly(typeof(PostgreDbContext).Assembly);
        }

        private static void InitEnums(ModelBuilder builder)
        {
            builder
                .HasPostgresEnum<ReactionType>(schema: "public", name: "reaction_type");
            builder
                .HasPostgresEnum<Status>(schema: "public", name: "status");
            builder
                .HasPostgresEnum<Role>(schema: "public", name: "role");
        }

        private static void InitRelations(ModelBuilder builder)
        {
            builder
                .Entity<User>()
                .HasMany(user => user.Publications)
                .WithOne(publ => publ.Issuer)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .Entity<User>()
                .HasMany(user => user.Favorites)
                .WithMany(publ => publ.Favourites);

            builder
                .Entity<User>()
                .HasMany(user => user.Comments)
                .WithOne(comment => comment.Issuer)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .Entity<User>()
                .HasMany(user => user.Reactions)
                .WithOne(reaction => reaction.Issuer)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .Entity<Publication>()
                .HasMany(p => p.Themes)
                .WithMany(t => t.Publications);

            builder
                .Entity<Publication>()
                .HasMany(p => p.Comments)
                .WithOne(c => c.Publication);

            builder
                .Entity<Publication>()
                .HasMany(p => p.Reactions)
                .WithOne(r => r.Publication);

            builder
                .Entity<Theme>()
                .HasOne(t => t.Category)
                .WithMany(c => c.Themes);

            builder
                .Entity<Comment>()
                .HasMany(c => c.Reactions)
                .WithOne(r => r.Comment);
        }
    }
}
