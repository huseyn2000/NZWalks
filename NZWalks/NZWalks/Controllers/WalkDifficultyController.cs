using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using NZWalks.Repositories;

namespace NZWalks.Controllers
{

    [ApiController]
    [Route("[controller]")]


    public class WalkDifficultyController : Controller
    {
        private readonly IWalkDifficultyRepository walkDifficultyRepository;
        private readonly IMapper mapper;

        public WalkDifficultyController(IWalkDifficultyRepository
            walkDifficultyRepository, IMapper mapper)
        {
            this.walkDifficultyRepository = walkDifficultyRepository;
            this.mapper = mapper;
        }

        [HttpGet]

        public async Task<IActionResult> getAll()
        {

            var domain = await walkDifficultyRepository.getAll();

            var Dto = mapper.Map<List<Model.DTO.WalkDifficulty>>(domain);

            return Ok(Dto);


        }

        [HttpGet]
        [Route("{id:guid}")]
        [ActionName("add_wk")]
        public async Task<IActionResult> getDifficulty(Guid id)
        {

            var domain = await walkDifficultyRepository.get(id);
            var Dto = mapper.Map<Model.DTO.WalkDifficulty>(domain);

            return Ok(Dto);

        }

        [HttpPost]

        public async Task<IActionResult> add_wk(Model.DTO.add_wd add_Wd)
        {


            var domain = new Models.Domain.WalkDifficulty()
            {
                Code = add_Wd.Code,

            };

            domain = await walkDifficultyRepository.add(domain);

            var dto = new Model.DTO.WalkDifficulty()
            {
                Id = domain.Id,
                Code = add_Wd.Code
            };

            return CreatedAtAction(nameof(add_wk), new { id = dto.Id }, dto);

        }


        [HttpPut]
        [Route("{id:guid}")]

        public async Task<IActionResult> update([FromRoute] Guid id,
            [FromBody] Model.DTO.add_wd update_Wd)
        {

            var domain = new Models.Domain.WalkDifficulty()
            {

                Code = update_Wd.Code
            };

           domain = await walkDifficultyRepository.update_wd(id, domain);

            if (domain == null)
            {
                return NotFound();

            }

            var dto = new Model.DTO.WalkDifficulty()
            {
                Id = domain.Id,
                Code = update_Wd.Code
            };

            return Ok(dto);


        }


        [HttpDelete]
        [Route("{id:guid}")]

        public async Task<IActionResult> update(Guid id)
        {
            var domain =  await walkDifficultyRepository.delete(id);

            if (domain == null)
            {
                return NotFound();

            }

            mapper.Map<Model.DTO.WalkDifficulty>(domain);

            return Ok(domain);

        }

    }
}
