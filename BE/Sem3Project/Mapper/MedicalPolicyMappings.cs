using AutoMapper;
using Sem3Project.Models;
using Sem3Project.Models.Dtos;

namespace Sem3Project.Mapper
{
    public class MedicalPolicyMappings: Profile
    {
        public MedicalPolicyMappings()
        {
            CreateMap<MedicalPolicy, MedicalPolicyDto>().ReverseMap();
        }
    }
}
