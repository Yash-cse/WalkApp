using Microsoft.EntityFrameworkCore;
using WalkApp.DAL.WalkApp.DAL.Data;
using WalkApp.DAL.WalkApp.DAL.Repositories.WalkApp.DAL.Repositories.Interface;
using WalkApp.Domain.WalkApp.Domain.Models;

namespace WalkApp.DAL.WalkApp.DAL.Repositories.WalkApp.DAL.Repositories.Sql
{
    public class SqlWalkRepository : IWalkRepository
    {
        private readonly WalkAppDbContext _dbContext;

        public SqlWalkRepository(WalkAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Walks>> GetAllWalkAsync(string? filterOn = null, string? filterQuery = null,
                                                       string? sortBy = null, bool isAscending = true,
                                                       int pageNumber = 1, int pageSize = 100)
        {
            //return await _dbContext.Walks.Include("Region").Include("Difficulty").ToListAsync();
            var Walks = _dbContext.Walks.Include("Region").Include("Difficulty").AsQueryable();

            //filtering the data 
            if(string.IsNullOrWhiteSpace(filterOn)==false && string.IsNullOrWhiteSpace(filterQuery)==false)
            {
                //Name
                if(filterOn.Equals("name", StringComparison.OrdinalIgnoreCase))
                {
                    Walks = Walks.Where(x => x.Name.Contains(filterQuery));
                }
                //Description
                else if (filterOn.Equals("Description", StringComparison.OrdinalIgnoreCase))
                {
                    Walks = Walks.Where(x => x.Description.Contains(filterQuery));
                }
                //LengthInKm
                else if (filterOn.Equals("LengthInKm", StringComparison.OrdinalIgnoreCase))
                {
                    if(double.TryParse(filterQuery, out double length))
                    {
                        Walks = Walks.Where(x => x.LengthInKm <= length);
                    }
                }
            }
            //Sorting
            if(string.IsNullOrWhiteSpace(sortBy) == false)
            {
                if(sortBy.Equals("name", StringComparison.OrdinalIgnoreCase))
                {
                    Walks = isAscending ? Walks.OrderBy(x => x.Name): Walks.OrderByDescending(x => x.Name); 
                }
                else if (sortBy.Equals("LengthInKm", StringComparison.OrdinalIgnoreCase))
                {
                    Walks = isAscending ? Walks.OrderBy(x => x.LengthInKm) : Walks.OrderByDescending(x => x.LengthInKm);
                }
            }

            //Pagination
            var skipResults = (pageNumber - 1) * pageSize;

            //Return
            return await Walks.Skip(skipResults).Take(pageSize).ToListAsync();
        }

        public async Task<Walks> GetWalkByIDAsync(Guid id)
        {
          return  await _dbContext.Walks.Include("Region").Include("Difficulty").FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Walks> CreateWalkAsync(Walks CreateWalk)
        {
            await _dbContext.Walks.AddAsync(CreateWalk);
            await _dbContext.SaveChangesAsync();
            return CreateWalk;
        }

        public async Task<Walks> UpdateWalkAsync(Guid id, Walks UpdateWalk)
        {
            var existingWalk = await _dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (existingWalk == null) return null;

            existingWalk.Name = UpdateWalk.Name;
            existingWalk.Description = UpdateWalk.Description;
            existingWalk.LengthInKm = UpdateWalk.LengthInKm;
            existingWalk.WalkImageUrl = UpdateWalk.WalkImageUrl;
            existingWalk.RegionId = UpdateWalk.RegionId;
            existingWalk.DifficultyId = UpdateWalk.DifficultyId;

            await _dbContext.SaveChangesAsync();
            return existingWalk;
        }

        public async Task<Walks> DeleteWalkAsync(Guid id)
        {
            var existingWalk = await _dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
            if (existingWalk == null) return null;

            _dbContext.Remove(existingWalk);
            await _dbContext.SaveChangesAsync();
            return existingWalk;
        }
    }
}
