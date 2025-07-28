namespace WalkApp.Domain.WalkApp.Domain.DTO.WalkApp.Domain.DTO.UpdateRequest
{
    public class UpdateWalkRequestDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double LengthInKm { get; set; }
        public string? WalkImageUrl { get; set; }
        public Guid RegionId { get; set; }
        public Guid DifficultyId { get; set; }
    }
}
