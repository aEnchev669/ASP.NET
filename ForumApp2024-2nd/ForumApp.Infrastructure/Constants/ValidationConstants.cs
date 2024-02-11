using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ForumApp24.Infrastructure.Constants
{
	public static class ValidationConstants
	{
		//Post Validations
		public const int TitleMinLength = 10;
		public const int TitleMaxLength = 50;

		public const int ContentMinLength = 30;
		public const int ContentMaxLength = 1500;


		//ErrorMessages
		public const string RequiredErrorMessage = "The {0} filed is required!";
		public const string StringLengthErrorMessage = "The {0} must be between {2} and {1} characters long!";
	}
}
