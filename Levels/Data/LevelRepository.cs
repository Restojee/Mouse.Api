using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking.Internal;
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
        var userId = request.UserId.GetValueOrDefault();
        var query = this.context.Levels
            .AsNoTracking()
            .Select(level => new LevelEntity
            {
                Id = level.Id,
                Description = level.Description,
                Name = level.Name,
                User = level.User,
                Tags = level.Tags,
                CompletedCount = level.Completed.Count,
                VisitsCount = level.Visits.Count,
                FavoritesCount = level.Favorites.Count,
                CommentsCount = level.Comments.Count,
                CreatedUtcDate = level.CreatedUtcDate,
                ModifiedUtcDate = level.ModifiedUtcDate,
                IsCompletedByUser = level.Completed.Select(c => c.User.Id).Any(user => user == userId),
                IsFavoriteByUser = level.Favorites.Select(f => f.User.Id).Any(user => user == userId),
                Image = level.Image,
            });

        if (request.UserId != null && request.IsCreatedByUser.GetValueOrDefault())
        {
            query = LevelRepositoryFilters.GetFilterByUserQuery(query, request.UserId.GetValueOrDefault());
        }
        
        if (request.IsCompleted != null)
        {
            query = LevelRepositoryFilters.GetFilterByCompletedQuery(this.context, query, request.UserId.GetValueOrDefault(), request.IsCompleted.GetValueOrDefault());
        }
        
        if (request.IsFavorite != null)
        {
            query = LevelRepositoryFilters.GetFilterByFavoriteQuery(this.context, query, request.UserId.GetValueOrDefault());
        }
        
        if (request.HasNote != null)
        {
            query = LevelRepositoryFilters.GetFilterByNoteQuery(this.context, query, request.UserId.GetValueOrDefault());
        }
        
        if (request.TagIds != null)
        {
            query = LevelRepositoryFilters.GetFilterByTags(query, request.TagIds);
        }
        
        if (request.Name != null)
        {
            query = LevelRepositoryFilters.GetFilterByName(query, request.Name);
        }

        var levels = await PaginationExtensions.ToPagedResult(query.OrderByDescending(level => level.CreatedUtcDate), request.Page, request.Size);

        return levels;
    }
    
    public async Task<LevelEntity?> GetLevel(int levelId, int? userId = null)
    {
        return await this.context.Levels
            .AsNoTracking()
            .Include(level => level.Completed)
            .ThenInclude(completed => completed.User)
            .Select(level => new LevelEntity
            {
                Id = level.Id,
                Description = level.Description,
                Name = level.Name,
                User = level.User,
                Tags = level.Tags,
                CreatedUtcDate = level.CreatedUtcDate,
                ModifiedUtcDate = level.ModifiedUtcDate,
                Completed = level.Completed,
                Notes = level.Notes.Where(note => note.User.Id == userId.GetValueOrDefault()).ToList(),
                CompletedCount = level.Completed.Select(c => c.Id).ToList().Count,
                VisitsCount = level.Visits.Select(v => v.Id).ToList().Count,
                FavoritesCount = level.Favorites.Select(f => f.Id).ToList().Count,
                CommentsCount = level.Comments.Select(c => c.Id).ToList().Count,
                IsCompletedByUser = level.Completed.Select(c => c.User.Id).Any(user => user == userId),
                IsFavoriteByUser = level.Favorites.Select(f => f.User.Id).Any(user => user == userId),
                Image = level.Image,
            })
            .Where(level => level.Id == levelId)
            .FirstOrDefaultAsync();
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
            if (await this.context.Tags.Where(tag => tag.Id == tagId).FirstOrDefaultAsync() != null)
            {
                await this.context.LevelTagRelations.AddAsync(new LevelTagRelation { TagId = tagId, LevelId = level.Id });
            }
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
        await this.context.LevelNotes.AddAsync(note);
        await this.context.SaveChangesAsync();
    }
    
    public async Task CompleteLevel(LevelCompletedEntity completed)
    {
        await this.context.LevelCompleted.AddAsync(completed);
        await this.context.SaveChangesAsync();
    }
    
    public async Task UnCompleteLevel(LevelCompletedEntity completed)
    {
        this.context.LevelCompleted.Remove(completed);
        await this.context.SaveChangesAsync();
    }
    
    public async Task FavoriteLevel(LevelFavoriteEntity favorite)
    {
        await this.context.LevelFavorites.AddAsync(favorite);
        await this.context.SaveChangesAsync();
    }
    
    public async Task UnFavoriteLevel(LevelFavoriteEntity favorite)
    {
        this.context.LevelFavorites.Remove(favorite);
        await this.context.SaveChangesAsync();
    }
    
    public async Task<LevelFavoriteEntity?> GetFavoriteLevel(int levelId, int userId)
    {
        return await this.context.LevelFavorites.Where(favorite => favorite.Level.Id == levelId && favorite.User.Id == userId).FirstOrDefaultAsync();
    }
    
    public async Task<LevelCompletedEntity?> GetCompletedLevel(int levelId, int userId)
    {
        return await this.context.LevelCompleted.Where(completed => completed.Level.Id == levelId && completed.User.Id == userId).FirstOrDefaultAsync();
    }
    
    public async Task CreateLevelVisit(LevelVisitEntity visit)
    {
        await this.context.LevelVisits.AddAsync(visit);
        await this.context.SaveChangesAsync();
    }
}