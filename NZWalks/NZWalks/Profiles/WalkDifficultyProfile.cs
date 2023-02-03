using AutoMapper;

namespace NZWalks.Profiles
{
    public class WalkDifficultyProfile:Profile  
    {
        public WalkDifficultyProfile()
        {
            CreateMap<Models.Domain.WalkDifficulty,Model.DTO.WalkDifficulty>()
            
                .ReverseMap();

            CreateMap<Model.DTO.add_wd,Models.Domain.WalkDifficulty > ()
                .ReverseMap();
        }


    }
}
