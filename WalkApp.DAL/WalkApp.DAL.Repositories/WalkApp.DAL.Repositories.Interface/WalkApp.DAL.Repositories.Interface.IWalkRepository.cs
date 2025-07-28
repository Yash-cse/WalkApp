using WalkApp.Domain.WalkApp.Domain.Models;

namespace WalkApp.DAL.WalkApp.DAL.Repositories.WalkApp.DAL.Repositories.Interface
{
    public interface IWalkRepository
    {
        Task<List<Walks>> GetAllWalkAsync();
        Task<Walks> GetWalkByIDAsync(Guid id);
        Task<Walks> CreateWalkAsync(Walks CreateWalk);
        Task<Walks> UpdateWalkAsync(Guid id, Walks UpdateWalk);
        Task<Walks> DeleteWalkAsync(Guid id);
    }
}
