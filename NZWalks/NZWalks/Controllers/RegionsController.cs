using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.Models.Domain;
using NZWalks.Repositories;

namespace NZWalks.Controllers
{

    [ApiController]
    [Route("[controller]")]

    public class RegionsController : Controller
    {
        private readonly IRegionRepository regionRepository;
        private readonly IMapper mapper;

        public RegionsController(IRegionRepository regionRepository, IMapper mapper)
        {
            this.regionRepository = regionRepository;
            this.mapper = mapper;
        }


        [HttpGet]

        public async Task<IActionResult> GetAllRegions()
        {
            //var regions = new List<Region>()
            //{

            //    new Region
            //    {
            //        Id = Guid.NewGuid(),
            //        Name =  "Wellington",
            //        Code = "WLG",
            //        Area = 227755,
            //        Lat = -1.8822,
            //        Long = 299.88,
            //        Population = 500000
            //    },
            //     new Region
            //    {
            //        Id = Guid.NewGuid(),
            //        Name =  "Auckland",
            //        Code = "AUCK",
            //        Area = 227755,
            //        Lat = -1.8822,
            //        Long = 299.88,
            //        Population = 500000
            //    }
            //};
            var regions = await regionRepository.GetAllAsync(); ;


            //// return DTO regions

            //var regionsDTO = new List<Model.DTO.Region>();
            //regions.ToList().ForEach(region =>
            //{

            //    var regionDTO = new Model.DTO.Region()
            //    {
            //        Id = region.Id,
            //        Name = region.Name,
            //        Code = region.Code,
            //        Area = region.Area,
            //        Lat = region.Lat,
            //        Long = region.Long,
            //        Population = region.Population,



            //    };
            //    regionsDTO.Add(regionDTO);

            //});

            var regionsDTO = mapper.Map<List<Model.DTO.Region>>(regions);
            return Ok(regionsDTO);
        }

        [HttpGet]
        [Route("{id:guid}")]

        public async Task<IActionResult> GetRegionAsync(Guid id)
        {
            var region = await regionRepository.GetAsync(id);

            if (region == null)
            {
                return NotFound();
            }

            var regionDTO = mapper.Map<Model.DTO.Region>(region);

            return Ok(regionDTO);
        }

        [HttpPost]
        [ActionName("AddRegionAsync")]

        public async Task<IActionResult> AddRegionAsync(Model.DTO.AddRegionRequest addRegionRequest)
        {
            //Request(DTO) to Domain model

            var region = new Models.Domain.Region()
            {
                Name = addRegionRequest.Name,
                Code = addRegionRequest.Code,
                Area = addRegionRequest.Area,
                Lat = addRegionRequest.Lat,
                Long = addRegionRequest.Long,
                Population = addRegionRequest.Population,

            };


            // Pass deatails to Repository

            region = await regionRepository.AddAsync(region);



            //Convert back to DTO
            var regionDTO = new Model.DTO.Region()
            {
                Id = region.Id,
                Name = region.Name,
                Code = region.Code,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Population = region.Population,

            };

            return CreatedAtAction(nameof(AddRegionAsync), new { id = regionDTO.Id }, regionDTO);
        }

        [HttpDelete]
        [Route("{id:guid}")]

        public async Task<IActionResult> DeleteregionAsync(Guid id)
        {
            //Get region from database

            var deleted = await regionRepository.DeleteAsync(id);


            // If null NotFound

            if (deleted == null)
            {
                return NotFound();
            }


            //Convert response back to DTO
            var regionDTO = new Model.DTO.Region()
            {
                Id = deleted.Id,
                Name = deleted.Name,
                Code = deleted.Code,
                Area = deleted.Area,
                Lat = deleted.Lat,
                Long = deleted.Long,
                Population = deleted.Population,

            };
            //return Ok response

            return Ok(regionDTO);
        }

        [HttpPut]
        [Route("{id:guid}")]

        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] Model.DTO.UpdatedRegionRequest updatedRegionRequest)
        {
            // Convert DTO to Domain Model

            var region = new Models.Domain.Region()
            {
                Name = updatedRegionRequest.Name,
                Code = updatedRegionRequest.Code,
                Area = updatedRegionRequest.Area,
                Lat = updatedRegionRequest.Lat,
                Long = updatedRegionRequest.Long,
                Population = updatedRegionRequest.Population,

            };



            // Update Region using repository

            region = await regionRepository.UpdateAsync(id, region);


            //If Null them NotFound

            if (region == null)
            {
                return null;
            }

            //Convert Domain back to DTO
            var regionDTO = new Model.DTO.Region()
            {

                Name = region.Name,
                Code = region.Code,
                Area = region.Area,
                Lat = region.Lat,
                Long = region.Long,
                Population = region.Population,

            };


            //return OK response

            return Ok(regionDTO);

        }






    }
}
