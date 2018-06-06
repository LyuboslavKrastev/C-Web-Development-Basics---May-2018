namespace CustomWebServer.ByTheCakeApplication.Data
{
    using Microsoft.EntityFrameworkCore;
    using Models;
    
    public class ByTheCakeDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Order> Orders { get; set; }
        
        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
           //builder
           //     .UseSqlServer(@"Server=.;Database=ByTheCakeDb;Integrated Security=True;");
            builder
                .UseSqlServer(@"Server=DESKTOP-F6QO428\SQLEXPRESS;Database=ByTheCakeDb;Integrated Security=True;");
        }
 
        protected override void OnModelCreating(ModelBuilder builder)
        {      
            builder.Entity<Order>()
               .HasMany(o => o.Products)
               .WithOne(op => op.Order)
               .HasForeignKey(op => op.OrderId);

            builder.Entity<OrderProduct>()
                .HasKey(op => new { op.OrderId, op.ProductId });

            builder.Entity<Product>()
                .HasMany(pr => pr.Orders)
                .WithOne(op => op.Product)
                .HasForeignKey(op => op.ProductId);

            builder.Entity<User>()
                .HasMany(u => u.Orders)
                .WithOne(o => o.User)
                .HasForeignKey(o => o.UserId);        
        }
    }
}
