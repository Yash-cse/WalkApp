using WalkApp.Domain.WalkApp.Domain.Models;

namespace WalkApp.DAL.WalkApp.DAL.Repositories.WalkApp.DAL.Repositories.Interface
{
    public interface IWalkRepository
    {
        Task<List<Walks>> GetAllWalkAsync(string? filterOn = null, string? filterQuery = null,
                                          string? sortBy = null,   bool isAscending = true,
                                          int pageNumber = 1, int pageSize = 100);
        Task<Walks> GetWalkByIDAsync(Guid id);
        Task<Walks> CreateWalkAsync(Walks CreateWalk);
        Task<Walks> UpdateWalkAsync(Guid id, Walks UpdateWalk);
        Task<Walks> DeleteWalkAsync(Guid id);
    }
}
