using Microsoft.EntityFrameworkCore;
using Mouse.NET.Common;
using Mouse.NET.Data;
using Mouse.NET.Data.Models;
using Mouse.NET.Users.Models;

namespace Mouse.NET.Users.Data;

public class UserRepository : IUserRepository
{
    private readonly MouseDbContext context;
    
    public UserRepository(MouseDbContext context)
    {
        this.context = context;
    }
    
    public async Task<PagedResult<UserEntity>> GetUserCollection(UserCollectionGetRequest request)
    {
        return await PaginationExtensions.ToPagedResult(
            this.context.Users.Select(user => new UserEntity()
            {
                Id = user.Id,
                UserName = user.UserName,
                Avatar = user.Avatar,
                CompletedCount = user.Completed.Select(c => c.Id).ToList().Count,
                FavoritesCount = user.Favorites.Select(f => f.Id).ToList().Count,
                LevelsCount = user.Levels.Select(l => l.Id).ToList().Count,
                CommentsCount = user.Comments.Select(c => c.Id).ToList().Count,
                CreatedUtcDate = user.CreatedUtcDate,
                ModifiedUtcDate = user.ModifiedUtcDate,
            }).AsQueryable(), 
            request.Page,
            request.Size
        );
    }
    
    public async Task<UserEntity?> GetUser(int userId)
    { 
       return await this.context.Users.FirstOrDefaultAsync(user => user.Id.Equals(userId));
    }

    public async Task<UserEntity?> GetUserWithStatistic(int userId)
    {
        return await this.context.Users.Select(user => new UserEntity()
        {
            Id = user.Id,
            UserName = user.UserName,
            Avatar = user.Avatar,
            CompletedCount = user.Completed.Select(c => c.Id).ToList().Count,
            FavoritesCount = user.Favorites.Select(f => f.Id).ToList().Count,
            LevelsCount = user.Levels.Select(l => l.Id).ToList().Count,
            CommentsCount = user.Comments.Select(c => c.Id).ToList().Count,
            CreatedUtcDate = user.CreatedUtcDate,
            ModifiedUtcDate = user.ModifiedUtcDate,
        }).FirstOrDefaultAsync(user => user.Id.Equals(userId));

    }
    
    public async Task<UserEntity?> GetUserByUserName(string userName)
    { 
        return await this.context.Users.Where(user => user.UserName == userName).FirstOrDefaultAsync();
    }

    public async Task<UserEntity?> CreateUser(UserEntity user)
    {
        await this.context.Users.AddAsync(user);
        await this.context.SaveChangesAsync();
        
        return await this.GetUser(user.Id);
    }

    public async Task<UserEntity?> UpdateUser(UserEntity user)
    {
        this.context.Entry(user).State = EntityState.Modified;
        await this.context.SaveChangesAsync();
        
        return await this.GetUser(user.Id);
    }

    public async Task DeleteUser(UserEntity user)
    {
        this.context.Users.Remove(user);
        await this.context.SaveChangesAsync();
    }
}