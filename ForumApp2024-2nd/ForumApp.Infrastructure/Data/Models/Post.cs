using ForumApp24.Infrastructure.Constants;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumApp24.Infrastructure.Data.Models
{
	public class Post
	{
        [Key]
        public int Id { get; set; }

        [Required]
		[MaxLength(ValidationConstants.TitleMaxLength)]
		public string Title { get; set; } = null!;

        [Required]
		[MaxLength(ValidationConstants.ContentMaxLength)]
		public string Content { get; set; } = null!;

    }
}
