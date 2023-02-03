using Microsoft.EntityFrameworkCore;
using NZWalks.Data;
using NZWalks.Models.Domain;

namespace NZWalks.Repositories
{
    public class WalkDifficultyRepository : IWalkDifficultyRepository
    {
        private readonly NZWalksDbContext nZWalksDbContext;

        public WalkDifficultyRepository(NZWalksDbContext nZWalksDbContext)
        {
            this.nZWalksDbContext = nZWalksDbContext;
        }

        public async Task<WalkDifficulty> add(WalkDifficulty walkDifficulty)
        {
            walkDifficulty.Id = Guid.NewGuid();
            var domain = await nZWalksDbContext.WalkDifficulties.AddAsync(walkDifficulty);
            nZWalksDbContext.SaveChangesAsync();
            return walkDifficulty;
        }

        public async Task<WalkDifficulty> delete(Guid id)
        {
            var walk_diff = await nZWalksDbContext.WalkDifficulties.FindAsync(id);

            if (walk_diff == null)
            {
                return null;

            }

            nZWalksDbContext.WalkDifficulties.Remove(walk_diff);
            nZWalksDbContext.SaveChangesAsync();
            return walk_diff;

        }

        public async Task<WalkDifficulty> get(Guid id)
        {
            var domain = await nZWalksDbContext.WalkDifficulties.FindAsync(id);

            return domain;

        }

        public async Task<IEnumerable<WalkDifficulty>> getAll()
        {

            return await nZWalksDbContext.WalkDifficulties.ToListAsync();


        }

        public async Task<WalkDifficulty> update_wd(Guid id,
        
            WalkDifficulty walkDifficulty)
        {
            var first = await nZWalksDbContext.WalkDifficulties.FindAsync(id);

            if (first == null)
            {
                return null;
            }

            first.Code = walkDifficulty.Code;
            await nZWalksDbContext.SaveChangesAsync();
            return first;

        }






    }
}
