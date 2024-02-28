using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SeminarHub.Data
{
    public class SeminarParticipant
    {
        [Comment("SeminarParticipant Seminar identifier")]
        [Required]
        public int SeminarId { get; set; }

        [ForeignKey(nameof(SeminarId))]
		[Comment("SeminarParticipant Seminar")]
		public Seminar Seminar { get; set; } = null!;

		[Comment("SeminarParticipant Participant identifier")]
		[Required]
        public string ParticipantId { get; set; } = null!;

		[Comment("SeminarParticipant Participant")]
		[ForeignKey(nameof(ParticipantId))]
        public IdentityUser Participant { get; set; } = null!;
    }
}