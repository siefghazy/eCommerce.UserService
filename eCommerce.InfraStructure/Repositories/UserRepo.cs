using eCommerce.Domian.Entities;
using eCommerce.Domian.Interface;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.InfraStructure.Repositories
{
    public class UserRepo : IUserRepository
    {
        private readonly DatabaseContext _context;
        public UserRepo(DatabaseContext context)
        {
            _context = context;
        }
        public async Task<ApplicationUser> AddUserAsync(ApplicationUser user)
        {
            await _context.User.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task DeleteUser(ApplicationUser user)
        {
            user.IsDeleted = true;
            await _context.SaveChangesAsync();
        }

        public IQueryable<ApplicationUser> GetAllUser()
        {
            var users = _context.User;
            return users;

        }

        public async Task<ApplicationUser?> GetUserByIdAsync(Guid id)
        {
            var user= await GetAllUser().FirstOrDefaultAsync(u => u.Id == id && u.IsDeleted == false);
            return user;
        }

        public async Task UpdateUser( ApplicationUser user)
        {
            
            user.Name = user.Name;
            user.Email = user.Email;
            user.Password = user.Password;
            user.Gender = user.Gender;
            user.IsDeleted = false;
            await _context.SaveChangesAsync();
        }

       public async Task<ApplicationUser> GetUserByEmailAndPasswordAsync(string email, string password)
        {
            var user= await GetAllUser().FirstOrDefaultAsync(u => u.Email == email && u.Password == password);
            return user;
        }
    }
}
