using FinancialManagementSystem.Core;
using Microsoft.EntityFrameworkCore;


namespace FinancialManagementSystem.Infrastructure
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User> AddUserAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task SignUpAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User> UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<bool> DeleteUserAsync(User user)
        {
            _context.Users.Remove(user);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<User?> UserGetDataAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        //public async Task<User?> UserGetDataByEmailAsync(string email)
        //{
        //    return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        //}

        public async Task<User?> UserGetDataByEmailAsync(string email)
        {
            try
            {
                return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching user by email: {ex.Message}");
                throw new ApplicationException("Unable to get user by email.", ex);
            }
        }

        public async Task<IEnumerable<User>> UserGetAllDataAsync()
        {
            return await _context.Users.ToListAsync();
        }
    }
}
