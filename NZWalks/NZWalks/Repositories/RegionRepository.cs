using Microsoft.EntityFrameworkCore;
using NZWalks.Data;
using NZWalks.Models.Domain;

namespace NZWalks.Repositories
{
    public class RegionRepository : IRegionRepository
    {
        private readonly NZWalksDbContext nZWalksDbContext;

        public RegionRepository(NZWalksDbContext nZWalksDbContext)
        {
            this.nZWalksDbContext = nZWalksDbContext;
        }

        public async Task<Region> AddAsync(Region region)
        {

            region.Id = Guid.NewGuid();
            await nZWalksDbContext.AddAsync(region);
            await nZWalksDbContext.SaveChangesAsync();
            return region;
        }

        public async Task<Region> DeleteAsync(Guid id)
        {
            var region = await nZWalksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (region == null)
            {
                return null;
            }

            nZWalksDbContext.Remove(region);
            nZWalksDbContext.SaveChangesAsync();
            return region;


        }

        public async Task<IEnumerable<Region>> GetAllAsync()
        {
            return await nZWalksDbContext.Regions.ToListAsync();
        }

        public async Task<Region> GetAsync(Guid id)
        {

            var region = await nZWalksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            return region;


        }

        public async Task<Region> UpdateAsync(Guid id, Region region)
        {


            var existingRegion = await nZWalksDbContext.Regions.FirstOrDefaultAsync(x => x.Id == id);

            if (existingRegion==null)
            {
                return null;

            }

            existingRegion.Walks = region.Walks;
            existingRegion.Area = region.Area;
            existingRegion.Lat = region.Lat;
            existingRegion.Code = region.Code;
            existingRegion.Long = region.Long;
            existingRegion.Name = region.Name;
            existingRegion.Population = region.Population;

            await nZWalksDbContext.SaveChangesAsync();
            return existingRegion;

        }
    }
}
