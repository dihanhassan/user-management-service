using AutoMapper;
using UserVault.Application.Dtos.User;
using UserVault.Domain.Entities;

namespace UserVault.Mapper
{
    public class ResponseMapper : Profile
    {
        public ResponseMapper()
        {
            CreateMap<User, UserDto>();
        }
    }
}
