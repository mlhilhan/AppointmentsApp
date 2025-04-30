using RandevuSistemi.Core.Entities;
using RandevuSistemi.Core.Repositories;
using RandevuSistemi.Core.Security;
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
        private readonly IPasswordHasher _passwordHasher;

        public UserService(IRepository<User> userRepository, IPasswordHasher passwordHasher)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
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
            var user = await GetUserByUsernameAsync(username);
            if (user == null)
                return false;

            // Eğer eski formatla şifre varsa ve passwordHash boşsa
            if (string.IsNullOrEmpty(user.PasswordHash) && user.Password == password)
            {
                // Kullanıcı giriş yaptığında şifreyi hash'e çevir
                user.PasswordHash = _passwordHasher.HashPassword(password);
                // Eski formatı temizle
                user.Password = null;
                await _userRepository.UpdateAsync(user);
                return true;
            }

            // Eski şifre yoksa veya passwordHash doluysa hash kontrolü yap
            if (!string.IsNullOrEmpty(user.PasswordHash))
            {
                return _passwordHasher.VerifyPassword(password, user.PasswordHash);
            }

            // Diğer durumlar için (geçici olarak)
            return user.Password == password;
        }

        public async Task<User> RegisterUserAsync(User user)
        {
            // Şifreyi hashle
            string passwordHash = _passwordHasher.HashPassword(user.Password);
            user.PasswordHash = passwordHash;
            // String şifre alanını temizle
            user.Password = null;

            await _userRepository.AddAsync(user);
            return user;
        }

        public async Task UpdateUserAsync(User user)
        {
            await _userRepository.UpdateAsync(user);
        }
    }
}