using System.ComponentModel.DataAnnotations;
using Workshop__ASP.NET_Core_Identity.Data;
using Task = Workshop__ASP.NET_Core_Identity.Data.Task;

namespace Workshop__ASP.NET_Core_Identity.Models
{
	public class BoardViewModel
	{
		public int Id { get; set; }

		[Required]
		[StringLength(DataConstants.BoardNameMaxLength,
			MinimumLength = DataConstants.BoardNameMinLength)]
		public string Name { get; set; } = string.Empty;

		public IEnumerable<TaskViewModel> Tasks { get; set; } = new List<TaskViewModel>();
	}
}
