using Sem3Project.Filters;
using Sem3Project.Models;
using Sem3Project.Helpers;
using Sem3Project.Models.Dtos;

namespace Sem3Project.Repositories
{
    public interface IUserRepository
    {
        PagedList<User> GetUsers(PaginationFilter paginationFilter, string search);
        User GetUser(string id);
        bool CreateUser(UserRegisterDto userRegisterDto);
        bool UpdateUser(UserUpdateDto userUpdateDto, string id);
        bool Save();

        User Login(UserLoginDto userLoginDto);

        bool ChangePassword(ChangePasswordDto changePasswordDto, string id);
    }
}
