using WalkApp.Domain.WalkApp.Domain.Models;

namespace WalkApp.DAL.WalkApp.DAL.Repositories.WalkApp.DAL.Repositories.Interface
{
    public interface IWalkRepository
    {
        Task<List<Walks>> GetAllWalkAsync();
        Task<Walks> GetWalkAsync(Guid id);
        Task<Walks> CreateWalkAsync(Walks walks);
        Task<Walks> DeleteWalkAsync(Guid id);
    }
}
