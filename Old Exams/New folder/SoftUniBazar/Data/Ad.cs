﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static SoftUniBazar.Data.DataConstants;
namespace SoftUniBazar.Data
{
    public class Ad
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(AdNameMaxLength)]
        public string Name { get; set; } = string.Empty;


        [Required]
        [MaxLength(AdDescriptionMaxLength)]
        public string Description { get; set; } = string.Empty;

        [Required]
        public decimal Price { get; set; }

        [Required]
        public string OwnerId { get; set; } = string.Empty;

        [Required]
        [ForeignKey(nameof(OwnerId))]
        public IdentityUser Owner { get; set; } = null!;

        [Required]
        public string ImageUrl { get; set; } = string.Empty;

        [Required]
        public DateTime CreatedOn { get; set; }

        [Required]
        public int CategoryId { get; set; }

        [Required]
        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; } = null!;
    }
}
