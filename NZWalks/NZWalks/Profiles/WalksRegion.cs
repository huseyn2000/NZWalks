using AutoMapper;

namespace NZWalks.Profiles
{
    public class WalksRegion : Profile
    {
        public WalksRegion()
        {
            CreateMap<Models.Domain.Walk, Model.DTO.Walk>()
                .ReverseMap();


            CreateMap<Models.Domain.WalkDifficulty, Model.DTO.WalkDifficulty>()
                .ReverseMap();

            CreateMap<Models.Domain.Walk, Model.DTO.Addwalk>()
      .ReverseMap();



        }

    }
}
