namespace NotesApp.Data
{
    using Microsoft.EntityFrameworkCore;
    using NotesApp.Models;

    public class NotesAppContext : DbContext
    {
        public DbSet<Note> Notes { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder builder)
        {
            if (!builder.IsConfigured)
            {
                builder.UseSqlServer(@"Server=localhost\SQLEXPRESS;Database=NotesAppDb;Integrated Security=True");
            }
            base.OnConfiguring(builder);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            builder.Entity<User>(entity =>
            {
                entity
                    .HasMany(u => u.Notes)
                    .WithOne(n => n.Author)
                    .HasForeignKey(n => n.AuthorId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            base.OnModelCreating(builder);
        }
    }
}
