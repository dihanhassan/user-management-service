using AutoMapper;
using UserVault.Application.Dtos.User;
using UserVault.Domain.Entities;

namespace UserVault.Mapper
{
    public class RequestMapper : Profile
    {
        public RequestMapper()
        {
            CreateMap<UserDto, User>();
        }
    }
}
