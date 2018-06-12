namespace CustomWebServer.GameStoreApplication.Data
{
    using Data.Models;
    using Microsoft.EntityFrameworkCore;

    public class GameStoreDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Game> Games { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder
               .UseSqlServer(@"Server=DESKTOP-F6QO428\SQLEXPRESS;Database=GameStoreDb;Integrated Security=True;");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<UserGame>()
                .HasKey(ug => new { ug.GameId, ug.UserId });

            builder.Entity<User>()
               .HasIndex(u => u.Email)
               .IsUnique();

            builder.Entity<User>()
                .HasMany(u => u.Games)
                .WithOne(ug => ug.User)
                .HasForeignKey(ug => ug.UserId);

            builder.Entity<Game>()
               .HasMany(g => g.Users)
               .WithOne(ug => ug.Game)
               .HasForeignKey(ug => ug.GameId);

        }
    }
}
