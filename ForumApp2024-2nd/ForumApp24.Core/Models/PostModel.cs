using ForumApp24.Infrastructure.Constants;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static ForumApp24.Infrastructure.Constants.ValidationConstants;

namespace ForumApp24.Core.Models
{
	public class PostModel
	{
		[Key]
		public int Id { get; set; }

		[Required(ErrorMessage = RequiredErrorMessage)]
		[StringLength(TitleMaxLength,
			MinimumLength = TitleMinLength,
			ErrorMessage = StringLengthErrorMessage)]
		public string Title { get; set; } = null!;

		[Required(ErrorMessage = RequiredErrorMessage)]
		[StringLength(ContentMaxLength,
			MinimumLength = ContentMinLength,
			ErrorMessage = StringLengthErrorMessage)]
		public string Content { get; set; } = null!;
	}
}
