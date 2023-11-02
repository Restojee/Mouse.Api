using Microsoft.EntityFrameworkCore;
using Mouse.NET.Common;
using Mouse.NET.Data;
using Mouse.NET.Data.Models;
using Mouse.NET.Levels.Models;

namespace Mouse.NET.Levels.Data;

public class LevelRepository : ILevelRepository
{
    private readonly MouseDbContext context;
    
    public LevelRepository(MouseDbContext context)
    {
        this.context = context;
    }
    
    public async Task<PagedResult<LevelEntity>> GetLevelCollection(LevelCollectionGetRequest request)
    {
        var query = this.context.Levels
            .Include(level => level.User)
            .Include(level => level.Comments)
            .Include(level => level.Tags)
            .Include(level => level.Completed)
            .Include(level => level.Favorites)
            .Include(level => level.Visits)
            .Include(level => level.Notes)
            .AsQueryable();

        if (request.IsCompleted != null)
        {
            query = LevelRepositoryFilters.GetFilterByCompletedQuery(this.context, query, request.UserId.GetValueOrDefault(), request.IsCompleted.GetValueOrDefault());
        }
        
        if (request.IsFavorite != null)
        {
            query = LevelRepositoryFilters.GetFilterByFavoriteQuery(this.context, query, request.UserId.GetValueOrDefault(), request.IsFavorite.GetValueOrDefault());
        }
        
        if (request.TagIds != null)
        {
            query = LevelRepositoryFilters.GetFilterByTags(query, request.TagIds);
        }
        
        if (request.Name != null)
        {
            query = LevelRepositoryFilters.GetFilterByName(query, request.Name);
        }

        var levels = await PaginationExtensions.ToPagedResult(query, request.Page, request.Size);
        
        foreach (var level in levels.Records)
        {
            level.IsCompletedByUser = level.Completed.Any(c => c.UserId == request.UserId.GetValueOrDefault());
            level.IsFavoriteByUser = level.Favorites.Any(f => f.UserId == request.UserId.GetValueOrDefault());
            level.CompletedCount = level.Completed.Count;
            level.FavoritesCount = level.Favorites.Count;
            level.CommentsCount = level.Comments.Count;
            level.VisitsCount = level.Visits.Count;
        }

        return levels;
    }
    
    public async Task<LevelEntity?> GetLevel(int levelId, int? userId = null)
    {
        var level =  await this.context.Levels 
           .Include(level => level.User)
           .Include(level => level.Comments)
           .Include(level => level.Tags)
           .Include(level => level.Completed)
           .Include(level => level.Favorites)
           .Include(level => level.Visits)
           .Include(level => level.Notes)
           .FirstOrDefaultAsync(level => level.Id.Equals(levelId));
       
       level.IsCompletedByUser = level.Completed.Any(c => c.UserId == userId.GetValueOrDefault());
       level.IsFavoriteByUser = level.Favorites.Any(f => f.UserId == userId.GetValueOrDefault());
       level.CompletedCount = level.Completed.Count;
       level.FavoritesCount = level.Favorites.Count;
       level.CommentsCount = level.Comments.Count;
       level.VisitsCount = level.Visits.Count;

       return level;
    }

    public async Task<LevelEntity?> CreateLevel(LevelEntity level)
    {
        this.context.Levels.Add(level);
        await this.context.SaveChangesAsync();
        
        return await this.GetLevel(level.Id);
    }

    public async Task<LevelEntity?> UpdateLevel(LevelEntity level)
    {
        this.context.Entry(level).State = EntityState.Modified;
        await this.context.SaveChangesAsync();
        
        return await this.GetLevel(level.Id);
    }

    public async Task DeleteLevel(LevelEntity level)
    {
        this.context.Levels.Remove(level);
        await this.context.SaveChangesAsync();
    }
    
    public async Task ClearLevelTags(int levelId)
    {
        this.context.LevelTagRelations.RemoveRange(this.context.LevelTagRelations.Where(relation => levelId == relation.LevelId));
        await this.context.SaveChangesAsync();
    }

    public async Task<LevelEntity?> SetLevelTags(LevelEntity level, ICollection<int> tagIds)
    {
        await ClearLevelTags(level.Id);
        foreach (var tagId in tagIds)
        {
            this.context.LevelTagRelations.Add(new LevelTagRelation
            {
                TagId = tagId,
                LevelId = level.Id
            });
        }
        await this.context.SaveChangesAsync();
        return await this.GetLevel(level.Id);
    }

    public async Task<LevelNoteEntity?> GetLevelNote(int levelId, int userId)
    {
        return await this.context.LevelNotes
            .Where(note => note.Level.Id == levelId && note.User.Id == userId)
            .FirstOrDefaultAsync();
    }

    public async Task UpdateLevelNote(LevelNoteEntity note)
    {
        this.context.Entry(note).State = EntityState.Modified;
        await this.context.SaveChangesAsync();
    }
    
    public async Task CreateLevelNote(LevelNoteEntity note)
    {
        this.context.LevelNotes.Add(note);
        await this.context.SaveChangesAsync();
    }
    
    public async Task CompleteLevel(LevelCompletedEntity completed)
    {
        this.context.LevelCompleted.Add(completed);
        await this.context.SaveChangesAsync();
    }
    
    public async Task UnCompleteLevel(LevelCompletedEntity completed)
    {
        this.context.LevelCompleted.Remove(completed);
        await this.context.SaveChangesAsync();
    }
    
    public async Task FavoriteLevel(LevelFavoriteEntity favorite)
    {
        this.context.LevelFavorites.Add(favorite);
        await this.context.SaveChangesAsync();
    }
    
    public async Task UnFavoriteLevel(LevelFavoriteEntity favorite)
    {
        this.context.LevelFavorites.Remove(favorite);
        await this.context.SaveChangesAsync();
    }
    
    public async Task<LevelFavoriteEntity?> GetFavoriteLevel(int levelId, int userId)
    {
        return await this.context.LevelFavorites.Where(
            favorite => favorite.LevelId == levelId && favorite.UserId == userId
        ).FirstOrDefaultAsync();
    }
    
    public async Task<LevelCompletedEntity?> GetCompletedLevel(int levelId, int userId)
    {
        return await this.context.LevelCompleted.Where(
            favorite => favorite.LevelId == levelId && favorite.UserId == userId
        ).FirstOrDefaultAsync();
    }
    
    public async Task CreateLevelVisit(LevelVisitEntity visit)
    {
        this.context.LevelVisits.Add(visit);
        await this.context.SaveChangesAsync();
    }
}