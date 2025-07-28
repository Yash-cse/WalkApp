using WalkApp.Domain.WalkApp.Domain.Models;

namespace WalkApp.DAL.WalkApp.DAL.Repositories.WalkApp.DAL.Repositories.Interface
{
    public interface IDifficultyRepository 
    {
        Task<List<Difficulty>> GetAllDifficultyAsync();
    }
}
