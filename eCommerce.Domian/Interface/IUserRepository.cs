using eCommerce.Domian.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Domian.Interface
{
    public interface IUserRepository
    {
        public IQueryable<ApplicationUser> GetAllUser();
        public Task<ApplicationUser?> GetUserByIdAsync(Guid id);
        public  Task<ApplicationUser> AddUserAsync(ApplicationUser user);
        public Task DeleteUser(ApplicationUser user);
        public Task UpdateUser( ApplicationUser user);
        public Task<ApplicationUser> GetUserByEmailAndPasswordAsync(string email,string password);

    }
}
