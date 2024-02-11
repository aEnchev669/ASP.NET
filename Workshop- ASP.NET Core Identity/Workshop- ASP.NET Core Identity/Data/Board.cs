using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using static Workshop__ASP.NET_Core_Identity.Data.DataConstants;

namespace Workshop__ASP.NET_Core_Identity.Data
{
	public class Board
	{
		[Key]
		[Comment("Board identifier")]
		public int Id { get; set; }

		[Required]
		[MaxLength(BoardNameMaxLength)]
		[Comment("Board name")]
		public string Name { get; set; } = null!;

        public IEnumerable<Task> Tasks { get; set; } = new List<Task>();
    }
}