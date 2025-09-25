
namespace FinancialManagementSystem.Core
{
    public interface IUserRepository
    {
        Task<User> AddUserAsync(User user);
        Task SignUpAsync(User user);
        Task<User> UpdateUserAsync(User existingUser);
        Task<bool> DeleteUserAsync(User user);
        Task<User> UserGetDataAsync(int id);
        Task<User> UserGetDataByEmailAsync(string email);
        Task<IEnumerable<User>> UserGetAllDataAsync();
    }
}
