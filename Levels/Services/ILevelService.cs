using Mouse.NET.Common;
using Mouse.NET.Levels.dto;
using Mouse.NET.Levels.Models;

namespace Mouse.NET.Levels.services;

public interface ILevelService
{
    public Task<PagedResult<Level>> GetLevelCollection(LevelCollectionGetRequest getRequest);

    public Task<Level> GetLevel(int levelId);

    public Task<Level> CreateLevel(LevelCreateRequest createRequest);

    public Task<Level> UpdateLevel(LevelUpdateRequest updateRequest);

    public Task<string> DeleteLevel(int levelId);

    public Task<Level> SetLevelTags(LevelTagsSetRequest request);

    public Task<Level> SetLevelNote(LevelNoteSetRequest request);

    public Task CompleteLevel(int levelId, IFormFile file, string? description);

    public Task UnCompleteLevel(int completedId);

    public Task FavoriteLevel(int levelId);

    public Task UnFavoriteLevel(int levelId);

    public Task UpdateLevelImage(int levelId, IFormFile file);
}