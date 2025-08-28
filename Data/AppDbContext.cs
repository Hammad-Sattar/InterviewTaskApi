using InterviewTask.Models;
using Microsoft.EntityFrameworkCore;

namespace InterviewTask.Data
    {
    public class AppDbContext : DbContext
        {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Book> Books { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
            base.OnModelCreating(modelBuilder);

            // Seed books
            modelBuilder.Entity<Book>().HasData(
                new Book { Id = 1, Title = "Harry Potter", Genre = "Fantasy"},
                new Book { Id = 2, Title = "Game of Thrones", Genre = "Fantasy" },
                new Book { Id = 3, Title = "The Hobbit", Genre = "Fantasy"  }
            );
            }
        }
    }
