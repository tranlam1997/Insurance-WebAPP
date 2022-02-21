using AutoMapper;
using Sem3Project.Models;
using Sem3Project.Models.Dtos;

namespace Sem3Project.Mapper
{
    public class LifePolicyMappings: Profile
    {
        public LifePolicyMappings()
        {
            CreateMap<LifePolicy, LifePolicyDto>().ReverseMap();
        }
    }
}
