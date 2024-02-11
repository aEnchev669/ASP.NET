using ForumApp24.Infrastructure.Data.Configuration;
using ForumApp24.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumApp24.Infrastructure.Data
{
	public class ForumDBContext : DbContext
	{
		public ForumDBContext(DbContextOptions<ForumDBContext> optionsBuilder)
			: base(optionsBuilder)
		{

		}
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.ApplyConfiguration<Post>(new PostConfiguration());
			base.OnModelCreating(modelBuilder);
		}

		public DbSet<Post> Posts { get; set; }
	}
}
