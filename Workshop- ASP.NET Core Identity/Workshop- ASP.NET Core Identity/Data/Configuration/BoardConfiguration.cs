using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Workshop__ASP.NET_Core_Identity.Data.Configuration
{
	public class BoardConfiguration : IEntityTypeConfiguration<Board>
	{
		public void Configure(EntityTypeBuilder<Board> builder)
		{
			builder
				.HasData(new Board[]
				{
					ConfigurationHelper.InProgressBoard,
					ConfigurationHelper.DoneBoard,
					ConfigurationHelper.OpenBoard
				});
		}
	}
}
