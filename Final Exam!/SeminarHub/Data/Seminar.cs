using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static SeminarHub.Data.DataConstants;
namespace SeminarHub.Data
{
    public class Seminar
    {
        [Comment("Seminar Identifier")]
        [Key]
        public int Id { get; set; }

		[Comment("Seminar Topic")]
		[Required]
        [MaxLength(SeminarTopicMaxLength)]
        public string Topic { get; set; } = null!;

		[Comment("Seminar Lecture")]
		[Required]
        [MaxLength(SeminarLecturerMaxLength)]
        public string Lecturer { get; set; } = null!;

		[Comment("Seminar Details")]
		[Required]
        [MaxLength(SeminarDetailsMaxLength)]
        public string Details { get; set; } = null!;

		[Comment("Seminar Organizer Identifier")]
		[Required]
        public string OrganizerId { get; set; } = null!;

		[Comment("Seminar Organizer")]
		[Required]
        [ForeignKey(nameof(OrganizerId))]
        public IdentityUser Organizer { get; set; } = null!;

        [Comment("Seminar Date")]
        [Required]
        public DateTime DateAndTime { get; set; }

		[Comment("Seminar Duration")]
		public int? Duration { get; set; }

		[Comment("Seminar Category Identifier")]
		[Required]
        public int CategoryId { get; set; }

		[Comment("Seminar Category")]
		[Required]
        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; } = null!;

		[Comment("Seminar Participants")]
		public ICollection<SeminarParticipant> SeminarsParticipants { get; set; } = new List<SeminarParticipant>();
    }
}
