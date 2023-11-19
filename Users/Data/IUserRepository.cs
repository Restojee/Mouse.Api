using Mouse.NET.Common;
using Mouse.NET.Data.Models;
using Mouse.NET.Users.Models;

namespace Mouse.NET.Users.Data
{
    public interface IUserRepository
    {
        public Task<PagedResult<UserEntity>> GetUserCollection(UserCollectionGetRequest request);
        
        public Task<UserEntity> GetUser(int userId);
        
        public Task<UserEntity> GetUserWithStatistic(int userId);
        
        public Task<UserEntity> GetUserByUserName(string userName);
        
        public Task<UserEntity> CreateUser(UserEntity user);
        
        public Task<UserEntity> UpdateUser(UserEntity user);
        
        public Task DeleteUser(UserEntity user);
    }
}