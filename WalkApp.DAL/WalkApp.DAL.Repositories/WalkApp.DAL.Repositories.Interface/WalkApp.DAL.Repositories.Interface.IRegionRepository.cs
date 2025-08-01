﻿using Region = WalkApp.Domain.WalkApp.Domain.Models.Region;

namespace WalkApp.DAL.WalkApp.DAL.Repositories.WalkApp.DAL.Repositories.Interface
{
    public interface IRegionRepository
    {
        Task<List<Region>> GetAllRegionsAsync();
        Task<Region> GetRegionByIdAsync(Guid id);
        Task<Region> CreateRegionAsync(Region region);
        Task<Region> UpdateRegionAsync(Guid id, Region region);
        Task<Region> DeleteRegionAsync(Guid id);
    }

    
}
