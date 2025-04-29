using RandevuSistemi.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RandevuSistemi.Core.Services
{
    public interface IUserService
    {
        Task<User> GetUserByIdAsync(int id);
        Task<User> GetUserByUsernameAsync(string username);
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<bool> ValidateUserAsync(string username, string password);
        Task<User> RegisterUserAsync(User user);
        Task UpdateUserAsync(User user);
    }
}
