using AutoMapper;
using Mouse.NET.Common;
using Mouse.NET.Data.Models;
using Mouse.NET.Users.Models;

namespace Mouse.NET.Users;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserEntity, User>();
        CreateMap<User, UserEntity>();
        CreateMap<UserCreateRequest, UserEntity>();
        CreateMap<PagedResult<UserEntity>, PagedResult<User>>();
        CreateMap<UserUpdateRequest, UserEntity>();
        CreateMap<UserAvatarUpdateRequest, UserEntity>();
    }
}