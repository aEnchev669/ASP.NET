using Library.Data;
using System.ComponentModel.DataAnnotations;
using static Library.Data.DataConstants;
namespace Library.Models
{
    public class BookFromViewModel
    {

        public int Id { get; set; }

        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(BookTitltMaxLength,
            MinimumLength = BookTitltMinLength,
            ErrorMessage = StringLengthErrorMessage)]
        public string Title { get; set; } = string.Empty;


        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(BookAuthorMaxLength,
            MinimumLength = BookAuthorMinLength,
            ErrorMessage = StringLengthErrorMessage)]
        public string Author { get; set; } = string.Empty;



        [Required(ErrorMessage = RequiredErrorMessage)]
        public string Url { get; set; } = string.Empty;


        [Required(ErrorMessage = RequiredErrorMessage)]
        [Range(BookRangeMinValue, BookRangeMaxValue)]
        public decimal Rating { get; set; }

        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(BookDescriptionMaxLength,
            MinimumLength = BookDescriptionMinLength,
            ErrorMessage = StringLengthErrorMessage)]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredErrorMessage)]
        [Range(0, int.MaxValue)]
        public int CategoryId { get; set; }

        public IEnumerable<Category> Categories { get; set; } = new List<Category>();
    }
}
