using Microsoft.EntityFrameworkCore;
using UserVault.Application.Interfaces;
using UserVault.Application.Static;
using UserVault.Data;
using UserVault.Domain.Entities;
using UserVault.Domain.Interfaces;

namespace UserVault.Infrastructure.Repositories
{
    public class UserRepo(
     ICacheService cacheService,
     EFDbContext context
     ) : IUserRepo
    {
        private readonly ICacheService _cache = cacheService;
        private readonly EFDbContext _context = context;
        private readonly DateTimeOffset _options = Helper.Helper.CreateCollectoCacheOptions();
        public async Task<List<User>?> GetAllAsync()
        {
            try
            {
                var response = new List<User>();
                var key = CacheKeyPattern.All;

                if (_cache.TryGetValue(key: key, value: out response) == false)
                {
                    response  = await _context.Users
                            .Where(u => !u.IsDeleted)
                            .ToListAsync();
                    //Cache
                    _ = _cache.Set(key: key, value: response, options: _options);
                }

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<User?> GetByIdAsync(int id)
        {
            try
            {
                var response = new User();

                var key = CacheKeyPattern.ById(id.ToString());

                if (_cache.TryGetValue(key: key, value: out response) == false)
                {
                    response = await _context.Users.FindAsync(id);
                    //Cache
                    _ = _cache.Set(key: key, value: response, options: _options);
                }

                return response;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<User> AddAsync(User user)
        {
            try
            {
                var rowEffect = await _context.AddAsync(user);
                await _context.SaveChangesAsync();
                _cache.Clear(CacheKeyPattern.Clear);
                return user;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<int> RemoveAsync(int id)
        {
            try
            {
                var cacheKey = CacheKeyPattern.ById(id.ToString());
                var response = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

                if (response == null)
                    return 0;

                response.IsDeleted = true;
                response.UpdatedOn = DateTime.UtcNow;
                _context.Users.Update(response);
                var result = await _context.SaveChangesAsync();

                _cache.Clear(CacheKeyPattern.Clear);
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<User?> UpdateAsync(User user)
        {
            try
            {
                var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.Id == user.Id);

                if (existingUser == null)
                    return null;

                #region map to update Data
                existingUser.FirstName = user.FirstName;
                existingUser.LastName = user.LastName;
                existingUser.Email = user.Email;
                existingUser.Mobile = user.Mobile;
                existingUser.Address = user.Address;
                existingUser.DateOfBirth = user.DateOfBirth;
                existingUser.UpdatedOn = DateTime.UtcNow;
                #endregion

                _context.Users.Update(existingUser);
                await _context.SaveChangesAsync();
                _cache.Clear(CacheKeyPattern.Clear);
                return existingUser;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
