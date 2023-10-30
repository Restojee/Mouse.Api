using Mouse.NET.Common;
using Mouse.NET.Users.Models;

namespace Mouse.NET.Users.services;

public interface IUserService
{
    public Task<PagedResult<User>> GetUserCollection(UserCollectionGetRequest getRequest);

    public Task<User> GetUser(int levelId);

    public Task<User> CreateUser(UserCreateRequest createRequest);

    public Task<User> UpdateUser(UserUpdateRequest updateRequest);

    public Task<string> DeleteUser(int levelId);
}