using KittenApp.Models;
using Microsoft.EntityFrameworkCore;

namespace KittenApp.Data
{
    public class KittenAppContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Kitten> Kittens { get; set; }

        public DbSet<Breed> Breeds { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                builder.UseSqlServer(SqlServerConnectionString.ConnectionString);
            }

            base.OnConfiguring(builder);
        }
    }
}
