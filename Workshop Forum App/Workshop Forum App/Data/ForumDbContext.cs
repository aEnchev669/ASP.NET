using Microsoft.EntityFrameworkCore;
using System.Net;
using Workshop_Forum_App.Data.Configuration;
using Workshop_Forum_App.Data.Models;

namespace Workshop_Forum_App.Data
{
    public class ForumDbContext: DbContext
    {
        public ForumDbContext(DbContextOptions<ForumDbContext> options)
            : base(options)
        {
            
        }

        public DbSet<Post> Posts { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PostEntityConfig ());

            base.OnModelCreating(modelBuilder);
        }
    }
}
