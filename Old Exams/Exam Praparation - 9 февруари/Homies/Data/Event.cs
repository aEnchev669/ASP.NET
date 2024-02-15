﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static Homies.Data.DataConstants;
using Type = Homies.Data.Type;

namespace Homies.Data
{
	public class Event
	{
		[Key]
		public int Id { get; set; }

		[Required]
		[MaxLength(EventNameMaxLength)]
		public string Name { get; set; } = string.Empty;

		[Required]
		[MaxLength(EventDescriptionMaxLength)]
		public string Description { get; set; } = string.Empty;

		[Required]
		public string OrganiserId { get; set; } = string.Empty;
		[Required]
		[ForeignKey(nameof(OrganiserId))]
		public IdentityUser Organiser { get; set; } = null!;

        [Required]
        public DateTime CreatedOn { get; set; }

		[Required]
		public DateTime Start { get; set; }

		[Required]
		public DateTime End { get; set; }

        [Required]
        public int TypeId { get; set; }
		[Required]
		[ForeignKey(nameof(TypeId))]
		public Type Type { get; set; } = null!;

		public ICollection<EventParticipant> EventParticipants { get; set; } = new List<EventParticipant>();
    }
}
