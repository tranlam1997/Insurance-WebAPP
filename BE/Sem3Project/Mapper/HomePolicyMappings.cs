using AutoMapper;
using Sem3Project.Models;
using Sem3Project.Models.Dtos;

namespace Sem3Project.Mapper
{
    public class HomePolicyMappings: Profile
    {
        public HomePolicyMappings()
        {
            CreateMap<HomePolicy, HomePolicyDto>().ReverseMap();
        }
    }
}
