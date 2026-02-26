using NewsPortal.Domain.Entities;
using NewsPortal.Domain.Interfaces;


namespace NewsPortal.Application.Services
{
    public class UserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> GetUserAsync(int id)
        {
            return await _userRepository.GetUserByIdAsync(id);
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetUsersAsync();
        }

        public async Task AddUserAsync(User user)
        {
            await _userRepository.AddUserAsync(user);
        }

        public async Task<User> LoginUser(User user)
        {
            return await _userRepository.LoginUser(user);
        }

        public async Task<User> UserValidateByEmail(string email)
        {
            return await _userRepository.UserValidateByEmailAsync(email);
        }
    }
}
