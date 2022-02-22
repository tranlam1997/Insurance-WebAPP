using AutoMapper;
using Sem3Project.Models;
using Sem3Project.Models.Dtos;

namespace Sem3Project.Mapper
{
    public class VehicleInsuranceMappings: Profile
    {
        public VehicleInsuranceMappings()
        {
            CreateMap<VehicleInsurance, VehicleInsuranceDto>().ReverseMap();
            CreateMap<VehicleInsurance, VehicleInsuranceForAdminDto>().ReverseMap();
        }
    }
}
