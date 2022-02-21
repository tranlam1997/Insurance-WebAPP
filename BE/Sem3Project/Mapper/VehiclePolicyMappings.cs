using AutoMapper;
using Sem3Project.Models;
using Sem3Project.Models.Dtos;

namespace Sem3Project.Mapper
{
    public class VehiclePolicyMappings: Profile
    {
        public VehiclePolicyMappings()
        {
            CreateMap<VehiclePolicy, VehiclePolicyDto>().ReverseMap();
        }
    }
}
