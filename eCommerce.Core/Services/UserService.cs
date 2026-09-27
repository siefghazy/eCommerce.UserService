using eCommerce.Core.DTO;
using eCommerce.Core.Interfaces;
using eCommerce.Domian.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Core.Services
{
     public class UserService:IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<GeneralResponse> GetAllUsersAsync()
        {
            try
            {
                IReadOnlyCollection<UsersDto> users = await _userRepository.GetAllUser().Where(x=>x.IsDeleted==false).Select(u => new UsersDto
                {
                    Id = u.Id,
                    Name = u.Name,
                    Email = u.Email,
                    Password = u.Password,
                    Gender = u.Gender
                }).ToListAsync();
                return new GeneralResponse
                {
                    Data = users,
                    Message = "Users retrieved successfully.",
                    StatusCode = System.Net.HttpStatusCode.OK
                };
            }
            catch(Exception ex)
            {
                return new GeneralResponse
                {
                    Data = null,
                    Message = "An error occurred while retrieving users.",
                    StatusCode = System.Net.HttpStatusCode.InternalServerError
                };
            }
        }

        public async Task<GeneralResponse> DeleteUser(Guid userId)
        {
            var user = await _userRepository.GetUserByIdAsync(userId);
            if (user == null)
            {
                return new GeneralResponse
                {
                    Data = null,
                    Message = "User not found.",
                    StatusCode = System.Net.HttpStatusCode.NotFound
                };
            }

            await _userRepository.DeleteUser(user);
            return new GeneralResponse
            {
                Data = null,
                Message = "User deleted successfully.",
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }

        public async Task<GeneralResponse> UpdateUserAsync(Guid userId, UsersDto user)
        {
            var existingUser = await _userRepository.GetUserByIdAsync(userId);
            if (existingUser == null)
            {
                return new GeneralResponse
                {
                    Data = null,
                    Message = "User not found.",
                    StatusCode = System.Net.HttpStatusCode.NotFound
                };
            }
            existingUser.Name = user.Name;
            existingUser.Email = user.Email;
            existingUser.Password = user.Password;
            existingUser.Gender = user.Gender;
            existingUser.IsDeleted = false;

            await _userRepository.UpdateUser(existingUser);
            return new GeneralResponse
            {
                Data = existingUser,
                Message = "User updated successfully.",
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        }
    }

