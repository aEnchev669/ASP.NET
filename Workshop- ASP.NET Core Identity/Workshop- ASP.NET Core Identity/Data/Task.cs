using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Workshop__ASP.NET_Core_Identity.Data.DataConstants;

namespace Workshop__ASP.NET_Core_Identity.Data
{
	[Comment("Board tasks")]
	public class Task
	{
		[Key]
		[Comment("Task identifier")]
		public int Id { get; set; }

		[Required]
		[MaxLength(TaskTitleMaxLength)]
		[Comment("Task title")]
		public string Title { get; set; } = string.Empty;

		[Required]
		[MaxLength(TaskDescriptionMaxLength)]
		[Comment("Task description")]
		public string Description { get; set; }= string.Empty;

		[Comment("Task creation")]
		public DateTime? CreatedOn { get; set; }

		[Comment("Bord identifier")]
		public int? BoardId { get; set; }

		[ForeignKey(nameof(BoardId))]
		public Board? Board { get; set; }

		[Required]
		[Comment("Application user identifier")]
		public string OwnerId { get; set; } = string.Empty;

		[ForeignKey(nameof(OwnerId))]
		public IdentityUser Owner { get; set; } = null!;
	}
}
