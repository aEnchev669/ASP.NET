using Homies.Data;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using static Homies.Data.DataConstants;
using Type = Homies.Data.Type;

namespace Homies.Models
{
    public class EventFormViewModel
    {
        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(EventNameMaxLength,
            MinimumLength = EventNameMinLength,
            ErrorMessage = StringLengthErrorMessage)]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredErrorMessage)]
        [StringLength(EventDescriptionMaxLength,
            MinimumLength = EventDescriptionMinLength,
            ErrorMessage = StringLengthErrorMessage)]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredErrorMessage)]
        public string Start { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredErrorMessage)]
        public string End { get; set; } = string.Empty;

        [Required(ErrorMessage = RequiredErrorMessage)]
        public int TypeId { get; set; }

        public IEnumerable<Type> Types { get; set; } = new List<Type>();
    }
}
