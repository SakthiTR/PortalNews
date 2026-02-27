using NewsPortal.Domain.Interfaces;
using NewsPortal.Domain.Entities;
using NewsPortal.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace NewsPortal.Infrastructure.Repositories
{
    public class UserRepository:IUserRepository
    {
         private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task<IEnumerable<User>> GetUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task AddUserAsync(User user)
        {
           await _context.Users.AddAsync(user);
           await _context.SaveChangesAsync();
        }

        public async Task<User> LoginUser(User user)
        {
            var userObject = _context.Users.Where(x => x.Email == user.Email && x.Password == user.Password && x.IsActive == true).FirstOrDefaultAsync().Result;
            return userObject;
        }

        public async Task<User> UserValidateByEmailAsync(string email)
        {
            return await _context.Users.Where(x => x.Email == email).FirstOrDefaultAsync();
        }
    }
}
