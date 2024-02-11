using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using static Workshop__ASP.NET_Core_Identity.Data.DataConstants;

namespace Workshop__ASP.NET_Core_Identity.Models
{
	public class TaskFormViewModel
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

		public int? BoardId { get; set; }

		public IEnumerable<TaskBoardViewModel> Boards { get; set; } = new List<TaskBoardViewModel>();
    }
}
