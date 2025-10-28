using AutoMapper;
using Azure;
using UserVault.Application.Dtos.Common;
using UserVault.Application.Dtos.User;
using UserVault.Application.Interfaces;
using UserVault.Domain.Entities;
using UserVault.Domain.Interfaces;

namespace UserVault.Application.Services
{
    public class UserService(
        IUserRepo repo,
        IMapper mapper
        ) : IUserService
    {
        private readonly IUserRepo _repo = repo;
        private readonly IMapper _mapper = mapper;
        public async Task<ApiResponse<UserDto>> AddAsync(UserDto userDto)
        {
            try
            {
                var finalResponse = new ApiResponse<UserDto>();
                var data = await _repo.AddAsync(_mapper.Map<User>(userDto));
                if (data != null)
                {
                    finalResponse.Data = _mapper.Map<UserDto>(data);
                    finalResponse.StatusCode = 200;
                    finalResponse.Message = "Success!";

                }
                else
                {
                    finalResponse.StatusCode = 200;
                    finalResponse.Message = "Failed!";
                }
                return finalResponse;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ApiResponse<List<UserDto>>> GetAllAsync()
        {
            try
            {
                var finalResponse = new ApiResponse<List<UserDto>>();
                var data = await _repo.GetAllAsync();
                if (data is { Count: > 0 })
                {
                    finalResponse.Data = _mapper.Map<List<UserDto>>(data);
                    finalResponse.StatusCode = 200;
                    finalResponse.Message = "Success!";

                }
                else
                {
                    finalResponse.StatusCode = 901;
                    finalResponse.Message = "No Data Found!";
                }
                return finalResponse;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ApiResponse<UserDto>> GetByIdAsync(int id)
        {
            try
            {
                var finalResponse = new ApiResponse<UserDto>();
                var data = await _repo.GetByIdAsync(id);
                if (data != null)
                {
                    finalResponse.Data = _mapper.Map<UserDto>(data);
                    finalResponse.StatusCode = 200;
                    finalResponse.Message = "Success!";

                }
                else
                {
                    finalResponse.StatusCode = 901;
                    finalResponse.Message = "Failed!";
                }
                return finalResponse;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ApiResponse<int>> RemoveAsync(int id)
        {
            try
            {
                var finalResponse = new ApiResponse<int>();
                var data = await _repo.RemoveAsync(id);
                if (data > 0)
                {
                    finalResponse.StatusCode = 200;
                    finalResponse.Message = "Success!";

                }
                else
                {
                    finalResponse.StatusCode = 901;
                    finalResponse.Message = "Failed!";
                }
                return finalResponse;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<ApiResponse<UserDto>> UpdateAsync(UserDto userDto)
        {
            try
            {
                var finalResponse = new ApiResponse<UserDto>();
                var data = await _repo.UpdateAsync(_mapper.Map<User>(userDto));
                if (data != null)
                {
                    finalResponse.Data = _mapper.Map<UserDto>(data);
                    finalResponse.StatusCode = 200;
                    finalResponse.Message = "Success!";

                }
                else
                {
                    finalResponse.StatusCode = 901;
                    finalResponse.Message = "Failed!";
                }
                return finalResponse;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
