using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.Repositories;

namespace NZWalks.Controllers
{


    [ApiController]
    [Route("[controller]")]

    public class WalksController : Controller
    {
        private readonly IWalkRepository walkRepository;
        private readonly IMapper mapper;

        public WalksController(IWalkRepository walkRepository, IMapper mapper)
        {
            this.walkRepository = walkRepository;
            this.mapper = mapper;
        }



        [HttpGet]

        public async Task<IActionResult> GetAll_walks()
        {
            var response = await walkRepository.GetAllWalk_async();

            var responseDTO = mapper.Map<List<Model.DTO.Walk>>(response);

            return Ok(responseDTO);

        }


        [HttpGet]
        [Route("{id:guid}")]


        public async Task<IActionResult> Get_walks(Guid id)
        {
            var walk = await walkRepository.GetRegion_id_async(id);

            var responseDTO = mapper.Map<Model.DTO.Walk>(walk);

            return Ok(responseDTO);

        }

        [HttpPost]

        public async Task<IActionResult> Add_walk(Model.DTO.Addwalk addwalk)
        {

            var domain_walk = mapper.Map<Models.Domain.Walk>(addwalk);

            await walkRepository.Add_Walks_async(domain_walk);

            var dto_walk = mapper.Map<Model.DTO.Walk>(domain_walk);

            return CreatedAtAction(nameof(Add_walk), new { id = dto_walk.Id }, dto_walk);



        }


        [HttpPut]
        [Route("{id:guid}")]


        public async Task<IActionResult> Update_walk([FromRoute] Guid id, [FromBody] Model.DTO.Updatewalk updatewalk)
        {

            var domain_walk = new Models.Domain.Walk
            {
                Length = updatewalk.Length,
                Name = updatewalk.Name,
                RegionId = updatewalk.RegionId,
                WalkDifficultyId = updatewalk.WalkDifficultyId


            };
            domain_walk = await walkRepository.Update_Walks_async(id, domain_walk);

            if (domain_walk == null)
            {
                return NotFound();

            }

            else
            {
                var dto_walk = new Model.DTO.Walk
                {
                    Id = domain_walk.Id,
                    Length = domain_walk.Length,
                    Name = domain_walk.Name,
                    RegionId = domain_walk.RegionId,
                    WalkDifficultyId = domain_walk.WalkDifficultyId
                };

                return Ok(dto_walk);
            }

        }


        [HttpDelete]
        [Route("{id:guid}")]

        public async Task<IActionResult> delete_walk(Guid id)
        {

            var deleted = await walkRepository.Delete_Walks_async(id);

            if (deleted == null)
            {
                return NotFound();
            }


            var dto_walk = new Model.DTO.Walk()
            {
                Name = deleted.Name,
                Length = deleted.Length,
                RegionId = deleted.RegionId,
                WalkDifficultyId = deleted.WalkDifficultyId

            };

            return Ok(dto_walk);



        }


    }
}
