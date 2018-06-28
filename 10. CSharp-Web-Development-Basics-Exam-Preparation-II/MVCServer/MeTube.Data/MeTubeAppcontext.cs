using MeTube.Models;
using Microsoft.EntityFrameworkCore;

namespace MeTube.Data
{
    public class MeTubeAppcontext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Tube> Tubes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                builder.UseSqlServer(SqlServerConnectionString.ConnectionString);
            }

            base.OnConfiguring(builder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<User>()
              .HasIndex(u => u.Email)
              .IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}
