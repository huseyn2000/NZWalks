using Microsoft.EntityFrameworkCore;
using NZWalks.Data;
using NZWalks.Models.Domain;

namespace NZWalks.Repositories
{
    public class WalkRepository : IWalkRepository
    {
        private readonly NZWalksDbContext nZWalksDbContext;

        public WalkRepository(NZWalksDbContext nZWalksDbContext)
        {
            this.nZWalksDbContext = nZWalksDbContext;
        }

        public async Task<Walk> Add_Walks_async(Walk walk)
        {

            walk.Id = Guid.NewGuid();
            await nZWalksDbContext.AddAsync(walk);

            await nZWalksDbContext.SaveChangesAsync();

            return walk;

        }

        public async Task<Walk> Delete_Walks_async(Guid id)
        {
            var deleted_walk = await nZWalksDbContext.Walks
                .FirstOrDefaultAsync(x => x.Id == id);

            if (deleted_walk == null)
            {
                return null;
            }

            nZWalksDbContext.Remove(deleted_walk);
            await nZWalksDbContext.SaveChangesAsync();

            return deleted_walk;

        }

        public async Task<IEnumerable<Walk>> GetAllWalk_async()
        {

            return await
                nZWalksDbContext.Walks
                .Include(x => x.Region)
                .Include(x => x.WalkDifficulty)

                .ToListAsync();
        }

        public async Task<Walk> GetRegion_id_async(Guid id)
        {

            return await
                nZWalksDbContext.Walks

                .Include(x => x.Region)
                .Include(x => x.WalkDifficulty)

                .FirstOrDefaultAsync(x => x.Id == id);

        }

        public async Task<Walk> Update_Walks_async(Guid id, Walk walk)
        {

            var a = await nZWalksDbContext.Walks.FirstOrDefaultAsync(x => x.Id == id);

            if (a == null)
            {

                return null;

            }

            a.Name = walk.Name;
            a.Length = walk.Length;
            a.WalkDifficultyId = walk.WalkDifficultyId;
            a.RegionId = walk.RegionId;


            nZWalksDbContext.SaveChangesAsync();

            return a;


        }
    }
}
