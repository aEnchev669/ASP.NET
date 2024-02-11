using Microsoft.AspNetCore.Identity;

namespace Workshop__ASP.NET_Core_Identity.Data.Configuration
{
	public static class ConfigurationHelper
	{
		public static IdentityUser TestUser = GetUser();

		public static Board OpenBoard = new Board()
		{
			Id = 1,
			Name = "Open",
		};

		public static Board InProgressBoard = new Board()
		{
			Id = 2,
			Name = "In Progress",
		};

		public static Board DoneBoard = new Board()
		{
			Id = 3,
			Name = "Done",
		};

		private static IdentityUser GetUser()
		{
			var haser = new PasswordHasher<IdentityUser>();

			var user = new IdentityUser()
			{
				UserName = "test@softuni.bg",
				NormalizedUserName = "TEST@SORTUNI.BG"
			};

			user.PasswordHash = haser.HashPassword(user, "softuni");

			return user;
		}
	}
}
