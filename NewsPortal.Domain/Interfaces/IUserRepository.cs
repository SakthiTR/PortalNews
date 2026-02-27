using NewsPortal.Domain.Entities;


namespace NewsPortal.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserByIdAsync(int id);
        Task<IEnumerable<User>> GetUsersAsync();
        Task AddUserAsync(User user);
        Task<User> LoginUser(User user);
        Task<User> UserValidateByEmailAsync(string email);
    }
}
