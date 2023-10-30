using Microsoft.EntityFrameworkCore;
using Mouse.NET.Data;
using Mouse.NET.Data.Models;
using Mouse.NET.LevelComments.Models;

namespace Mouse.NET.LevelComments.Data;

public class LevelCommentRepository : ILevelCommentRepository
{
    private readonly MouseDbContext context;
    
    public LevelCommentRepository(MouseDbContext context)
    {
        this.context = context;
    }
    
    public async Task<ICollection<LevelCommentEntity>> GetLevelCommentCollection(int levelId)
    {
        return await this.context.LevelComments.Where(level => level.Level.Id == levelId).Include(comment => comment.User).ToListAsync();
    }
    
    public async Task<LevelCommentEntity?> GetLevelComment(int levelId)
    { 
       return await this.context.LevelComments.Include(comment => comment.User).FirstOrDefaultAsync(level => level.Id.Equals(levelId));
    }

    public async Task<LevelCommentEntity?> CreateLevelComment(LevelCommentEntity level)
    {
        await this.context.LevelComments.AddAsync(level);
        await this.context.SaveChangesAsync();
        
        return await this.GetLevelComment(level.Id);
    }

    public async Task<LevelCommentEntity?> UpdateLevelComment(LevelCommentEntity level)
    {
        this.context.Entry(level).State = EntityState.Modified;
        await this.context.SaveChangesAsync();
        
        return await this.GetLevelComment(level.Id);
    }

    public async Task DeleteLevelComment(LevelCommentEntity level)
    {
        this.context.LevelComments.Remove(level);
        await this.context.SaveChangesAsync();
    }

    public async Task<ICollection<LevelCommentEntity>> GetLevelCommentsById(ICollection<int> levelCommentIds)
    {
        return await this.context.LevelComments.Where(levelComment => levelCommentIds.Contains(levelComment.Id)).ToListAsync();
    }

}