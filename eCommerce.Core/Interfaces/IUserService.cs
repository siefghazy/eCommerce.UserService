using eCommerce.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Core.Interfaces
{
    public interface IUserService
    {
        public Task<GeneralResponse> GetAllUsersAsync();

        public Task<GeneralResponse> DeleteUser(Guid userId);
        public Task<GeneralResponse> UpdateUserAsync(Guid userId, UsersDto user);
    }
}
