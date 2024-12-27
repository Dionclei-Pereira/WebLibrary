using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebLibrary.Entities;

namespace WebLibrary.Data {
    public class LibraryContext : IdentityDbContext {
        public LibraryContext(DbContextOptions options) : base (options) {}

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Book> Books { get; set; } = null!;
        protected override void OnModelCreating(ModelBuilder modelBuilder) {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<User>().ToTable("AspNetUsers");
        }
    }
}
