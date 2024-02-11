using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Build.Execution;
using Microsoft.EntityFrameworkCore;
using Workshop__ASP.NET_Core_Identity.Data.Configuration;

namespace Workshop__ASP.NET_Core_Identity.Data
{
	public class TaskBoardAppDbContext : IdentityDbContext
	{
		public TaskBoardAppDbContext(DbContextOptions<TaskBoardAppDbContext> options)
			: base(options)
		{
		}

		protected override void OnModelCreating(ModelBuilder builder)
		{
			builder.ApplyConfiguration(new UserConfiguration());
			builder.ApplyConfiguration(new BoardConfiguration());
			builder.ApplyConfiguration(new TaskConfiguration());

			base.OnModelCreating(builder);
		}

		public DbSet<Task> Tasks { get; set; }
        public DbSet<Board> Boards { get; set; } 
    }
}