using Sem3Project.Data;
using Sem3Project.Filters;
using Sem3Project.Helpers;
using Sem3Project.Models;
using Sem3Project.Models.Dtos;
using System;
using System.Linq;
using BC = BCrypt.Net.BCrypt;

namespace Sem3Project.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;

        public UserRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public PagedList<User> GetUsers(PaginationFilter paginationFilter, string search)
        {
            if (string.IsNullOrWhiteSpace(search))
            {
                return PagedList<User>.ToPagedList(
                    _db.Users.OrderBy(u => u.CreatedDate).ToList(),
                    paginationFilter.PageNumber,
                    paginationFilter.PageSize
                );
            }
            else
            {
                return PagedList<User>.ToPagedList(
                    _db.Users
                        .OrderBy(u => u.CreatedDate)
                        .Where(
                            u =>
                                u.Email.ToLower().Contains(search.Trim().ToLower())
                                || u.PhoneNumber.Contains(search.Trim().ToLower())
                        )
                        .ToList(),
                    paginationFilter.PageNumber,
                    paginationFilter.PageSize
                );
            }

            //return _db.Users
            //    .OrderBy(u => u.CreatedDate)
            //    .Skip((paginationFilter.PageNumber - 1) * paginationFilter.PageSize)
            //    .Take(paginationFilter.PageSize)
            //    .ToList();
        }

        public User GetUser(string id)
        {
            try
            {
                Guid guid = Guid.Parse(id);
                var user = _db.Users.FirstOrDefault(u => u.Id == guid);
                return user;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public bool CreateUser(UserRegisterDto userRegisterDto)
        {
            if (_db.Users.Any(u => u.Email == userRegisterDto.Email))
            {
                throw new Exception("Email already taken");
            }
            else
            {
                var user = new User();

                user.Email = userRegisterDto.Email;
                user.Password = BC.HashPassword(userRegisterDto.Password);
                user.DateOfBirth = userRegisterDto.DateOfBirth;
                user.FirtsName = userRegisterDto.FirtsName;
                user.LastName = userRegisterDto.LastName;
                user.PhoneNumber = userRegisterDto.PhoneNumber;
                user.Address = userRegisterDto.Address;
                user.CreatedDate = DateTime.Now;
                user.ModifiedDate = DateTime.Now;
                user.Role = "User";

                _db.Users.Add(user);
                return Save();
            }
        }

        public bool UpdateUser(UserUpdateDto userUpdateDto, string id)
        {
            User existUser = _db.Users.Where(u => u.Id == Guid.Parse(id)).FirstOrDefault();

            if (existUser != null)
            {
                existUser.DateOfBirth = userUpdateDto.DateOfBirth;
                existUser.FirtsName = userUpdateDto.FirtsName;
                existUser.LastName = userUpdateDto.LastName;
                existUser.PhoneNumber = userUpdateDto.PhoneNumber;
                existUser.Address = userUpdateDto.Address;
                existUser.ModifiedDate = DateTime.Now;
                _db.Users.Update(existUser);
                return Save();
            }
            else
            {
                return false;
            }
        }

        public bool Save()
        {
            return _db.SaveChanges() > 0;
        }

        public User Login(UserLoginDto userLoginDto)
        {
            var user = _db.Users.SingleOrDefault(u => u.Email == userLoginDto.Email);

            if (user == null || !BC.Verify(userLoginDto.Password, user.Password))
            {
                return null;
            }

            return user;
        }

        public bool ChangePassword(ChangePasswordDto changePasswordDto, string id)
        {
            User existUser = _db.Users.Where(u => u.Id == Guid.Parse(id)).FirstOrDefault();

            if (existUser == null || !BC.Verify(changePasswordDto.Password, existUser.Password))
            {
                return false;
            }

            existUser.Password = BC.HashPassword(changePasswordDto.NewPassword);
            _db.Users.Update(existUser);
            return Save();
        }

        //private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        //{
        //    if (password == null) throw new ArgumentNullException("password");
        //    if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

        //    using (var hmac = new System.Security.Cryptography.HMACSHA512())
        //    {
        //        passwordSalt = hmac.Key;
        //        passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        //    }
        //}
    }
}
