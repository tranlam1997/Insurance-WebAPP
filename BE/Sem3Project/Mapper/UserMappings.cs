using AutoMapper;
using Sem3Project.Models;
using Sem3Project.Models.Dtos;

namespace Sem3Project.Mapper
{
    public class UserMappings: Profile
    {
        public UserMappings()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<User, UserForAdminDto>().ReverseMap();
        }
    }
}
