using RandevuSistemi.Core.Entities;
using RandevuSistemi.Core.Repositories;
using RandevuSistemi.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandevuSistemi.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;

        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task<User> GetUserByUsernameAsync(string username)
        {
            var users = await _userRepository.FindAsync(u => u.UserName == username);
            return users.FirstOrDefault();
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task<bool> ValidateUserAsync(string username, string password)
        {
            var users = await _userRepository.FindAsync(u =>
                u.UserName == username && u.Password == password);
            return users.Any();
        }

        public async Task<User> RegisterUserAsync(User user)
        {
            await _userRepository.AddAsync(user);
            return user;
        }

        public async Task UpdateUserAsync(User user)
        {
            await _userRepository.UpdateAsync(user);
        }
    }
}
