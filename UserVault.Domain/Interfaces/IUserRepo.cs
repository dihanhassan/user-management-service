using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserVault.Domain.Entities;

namespace UserVault.Domain.Interfaces
{
    public interface IUserRepo
    {
        Task<List<User>?> GetAllAsync();
        Task<User?> UpdateAsync(User user);
        Task<int> RemoveAsync(int id);
        Task<User> AddAsync(User user);
        Task<User?> GetByIdAsync(int id);
    }
}
