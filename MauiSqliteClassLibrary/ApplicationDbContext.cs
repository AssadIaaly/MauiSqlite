using MauiSqliteClassLibrary.Models;
using Microsoft.EntityFrameworkCore;

namespace MauiSqliteClassLibrary
{
    public class ApplicationDbContext : DbContext
    {
        // Constructor with no argument is required and it is used when adding/removing migrations from class library
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
            //Database.EnsureCreated();
            Database.Migrate();
        }

        // It is required to override this method when adding/removing migrations from class library
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite();

        public DbSet<Post> Posts { get; set; }
    }
}
