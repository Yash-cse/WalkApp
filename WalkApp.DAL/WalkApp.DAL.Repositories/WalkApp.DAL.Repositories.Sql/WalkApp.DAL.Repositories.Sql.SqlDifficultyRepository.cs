using Microsoft.EntityFrameworkCore;
using WalkApp.DAL.WalkApp.DAL.Data;
using WalkApp.DAL.WalkApp.DAL.Repositories.WalkApp.DAL.Repositories.Interface;
using WalkApp.Domain.WalkApp.Domain.Models;

namespace WalkApp.DAL.WalkApp.DAL.Repositories.WalkApp.DAL.Repositories.Sql
{
    public class SqlDifficultyRepository : IDifficultyRepository
    {
        private readonly WalkAppDbContext _dbContext;

        public SqlDifficultyRepository(WalkAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Difficulty>> GetAllDifficultyAsync()
        {
            return await _dbContext.Difficulties.ToListAsync();
        }
    }
}
