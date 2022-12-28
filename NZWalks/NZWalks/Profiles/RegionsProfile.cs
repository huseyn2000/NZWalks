using AutoMapper;

namespace NZWalks.Profiles
{
    public class RegionsProfile : Profile
    {
        public RegionsProfile()
        {

            CreateMap<Models.Domain.Region, Model.DTO.Region>();


        }


    }
}
