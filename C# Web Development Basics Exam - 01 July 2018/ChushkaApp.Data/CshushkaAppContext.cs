using ChushkaApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ChushkaApp.Data
{
    public class CshushkaAppContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductType> ProductTypes { get; set; }
        public DbSet<Order> Orders { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                string connectionString = @"Server=localhost\SQLEXPRESS;Database=ChushkaDb_LyuboslavKrastev;Integrated Security=True";
                optionsBuilder.UseSqlServer(connectionString);

            }

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
            .Entity<Order>()
             .HasKey(o => new {o.Id, o.ClientId, o.ProductId});

            builder
                .Entity<Order>()
                .HasOne(o => o.Client)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.ClientId);

            builder
                .Entity<Order>()
                .HasOne(o => o.Product)
                .WithMany(g => g.Orders)
                .HasForeignKey(o => o.ProductId);
        }
    }
}
