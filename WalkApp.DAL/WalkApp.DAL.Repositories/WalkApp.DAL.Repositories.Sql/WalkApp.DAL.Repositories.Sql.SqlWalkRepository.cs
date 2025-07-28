using Microsoft.EntityFrameworkCore;
using WalkApp.DAL.WalkApp.DAL.Data;
using WalkApp.DAL.WalkApp.DAL.Repositories.WalkApp.DAL.Repositories.Interface;
using WalkApp.Domain.WalkApp.Domain.DTO.WalkApp.Domain.DTO.New;
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

        public async Task<List<Walks>> GetAllWalkAsync()
        {
            return await _dbContext.Walks.Include("Region").Include("Difficulty").ToListAsync();
        }

        public async Task<Walks> GetWalkAsync(Guid id)
        {
          return  await _dbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Walks> CreateWalkAsync(Walks walk)
        {
            await _dbContext.Walks.AddAsync(walk);
            await _dbContext.SaveChangesAsync();
            return walk;
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
