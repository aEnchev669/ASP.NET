using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Workshop__ASP.NET_Core_Identity.Data.Configuration
{
	public class UserConfiguration : IEntityTypeConfiguration<IdentityUser>
	{
		public void Configure(EntityTypeBuilder<IdentityUser> builder)
		{
			builder
				.HasData(ConfigurationHelper.TestUser);
		}
	}
}
