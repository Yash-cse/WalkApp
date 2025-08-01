﻿using System.ComponentModel.DataAnnotations;

namespace WalkApp.Domain.WalkApp.Domain.DTO.WalkApp.Domain.DTO.AddRequest
{
    public class AddWalkRequestDto
    {
        [Required]
        [MaxLength(100, ErrorMessage = "Name Has to be maximum of 100 characters")]
        public string Name { get; set; }

        [Required]
        [MaxLength(1000, ErrorMessage = "Description can only have maximum of 1000 characters")]
        public string Description { get; set; }

        [Required]
        [Range(0,50)]
        public double LengthInKm { get; set; }

        public string? WalkImageUrl { get; set; }

        [Required]
        public Guid RegionId { get; set; }

        [Required]
        public Guid DifficultyId { get; set; }

    }
}
