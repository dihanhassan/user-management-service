using UserVault.Application.Dtos.Common;
using UserVault.Application.Dtos.User;

namespace UserVault.Application.Interfaces
{
    public interface IUserService
    {
        Task<ApiResponse<List<UserDto>>> GetAllAsync();
        Task<ApiResponse<UserDto>> UpdateAsync(UserDto user);
        Task<ApiResponse<int>> RemoveAsync(int id);
        Task<ApiResponse<UserDto>> AddAsync(UserDto user);
        Task<ApiResponse<UserDto>> GetByIdAsync(int id);
    }
}
