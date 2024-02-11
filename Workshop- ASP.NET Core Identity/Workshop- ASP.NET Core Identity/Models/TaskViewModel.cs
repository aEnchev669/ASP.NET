using System.ComponentModel.DataAnnotations;
using static Workshop__ASP.NET_Core_Identity.Data.DataConstants;

namespace Workshop__ASP.NET_Core_Identity.Models
{
	public class TaskViewModel
	{
		public int Id { get; set; }

		[Required]
		[MaxLength(TaskTitleMaxLength)]
		[MinLength(TaskTitleMinLength)]
		public string Title { get; set; } = string.Empty;

		[Required]
		[MaxLength(TaskDescriptionMaxLength)]
		[MinLength(TaskDescriptionMinLength)]
		public string Description { get; set; } = string.Empty;

		[Required]
		public string Owner { get; set; } = string.Empty;
	}
}
