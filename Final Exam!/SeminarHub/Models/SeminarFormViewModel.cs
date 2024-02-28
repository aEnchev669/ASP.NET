using Microsoft.AspNetCore.Identity;
using SeminarHub.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static SeminarHub.Data.DataConstants;

namespace SeminarHub.Models
{
    public class SeminarFormViewModel
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(SeminarTopicMaxLength,
            MinimumLength = SeminarTopicMinLength,
            ErrorMessage = StringLengthErrorMessage)]
        public string Topic { get; set; } = null!;

        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(SeminarLecturerMaxLength,
            MinimumLength = SeminarLecturerMinLength,
            ErrorMessage = StringLengthErrorMessage)]
        public string Lecturer { get; set; } = null!;

        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(SeminarDetailsMaxLength,
            MinimumLength = SeminarDetailsMinLength,
            ErrorMessage = StringLengthErrorMessage)]
		public string Details { get; set; } = null!;

        [Required(ErrorMessage = RequiredErrorMessage)]
        public string DateAndTime { get; set; } = null!;

        [Range(SeminarDurationMinRange, SeminarDurationMaxRange)]
        public int? Duration { get; set; }

        [Required(ErrorMessage = RequiredErrorMessage)]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = RequiredErrorMessage)]
        public IEnumerable<Category> Categories { get; set; }= new List<Category>();


    }
}
