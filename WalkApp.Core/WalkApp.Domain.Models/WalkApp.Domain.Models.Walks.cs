﻿namespace WalkApp.Domain.WalkApp.Domain.Models
{
    public class Walks
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }

        
        // Relationship Between Models
        public Guid RegionId { get; set; }
        public Guid DifficultyId { get; set; }

        // Nevagation Prop
        public Region Region { get; set; }
        public Difficulty Difficulty { get; set; }


    }
}
