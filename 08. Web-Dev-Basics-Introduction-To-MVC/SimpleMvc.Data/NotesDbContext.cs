namespace SimpleMvc.Data
{
    using Microsoft.EntityFrameworkCore;
    using SimpleMvc.Domain;

    public class NotesDbContext : DbContext
    {
        public DbSet<Note> Notes { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            builder.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=NotesAppDb;Integrated Security=True");
            base.OnConfiguring(builder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>(entity =>
            {
                entity
                    .HasMany(u => u.Notes)
                    .WithOne(n => n.User)
                    .HasForeignKey(n => n.UserId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            base.OnModelCreating(builder);
        }
    }
}
