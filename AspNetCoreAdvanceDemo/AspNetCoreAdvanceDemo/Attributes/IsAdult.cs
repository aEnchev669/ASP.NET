using System.ComponentModel.DataAnnotations;

namespace AspNetCoreAdvanceDemo.Attributes
{
	public class IsAdult : ValidationAttribute
	{
		private readonly DateTime minimumAge = DateTime.Today.AddYears(-18);

		protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
		{
			if (value != null && (DateTime)value <= minimumAge)
			{
				return ValidationResult.Success;
			}

			return new ValidationResult(ErrorMessage);
		}
	}
}
