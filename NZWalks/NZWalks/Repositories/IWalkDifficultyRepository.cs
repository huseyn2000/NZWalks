using NZWalks.Models.Domain;

namespace NZWalks.Repositories
{
    public interface IWalkDifficultyRepository
    {

        Task<IEnumerable<WalkDifficulty>> getAll(); 

        Task<WalkDifficulty> get(Guid id);

        Task<WalkDifficulty> add(WalkDifficulty walkDifficulty);
        Task<WalkDifficulty> update_wd(Guid id, WalkDifficulty walkDifficulty);

        Task<WalkDifficulty> delete(Guid id);




    }
}
