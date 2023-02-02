using NZWalks.Models.Domain;

namespace NZWalks.Repositories
{
    public interface IWalkRepository
    {
        Task<IEnumerable<Walk>> GetAllWalk_async();

        Task<Walk> GetRegion_id_async(Guid id);
        
        Task<Walk> Add_Walks_async(Walk walk);
        Task<Walk> Update_Walks_async(Guid id, Walk walk);

        Task<Walk> Delete_Walks_async(Guid id);

    }
}
