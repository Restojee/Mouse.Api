using Mouse.NET.Common;
using Mouse.NET.Data.Models;
using Mouse.NET.Levels.Models;

namespace Mouse.NET.Levels.Data
{
    public interface ILevelRepository
    {
        public Task<PagedResult<LevelEntity>> GetLevelCollection(LevelCollectionGetRequest request);
        
        public Task<LevelEntity> GetLevel(int levelId, int? userId = null);
        
        public Task<LevelEntity> CreateLevel(LevelEntity level);
        
        public Task<LevelEntity> UpdateLevel(LevelEntity level);
        
        public Task DeleteLevel(LevelEntity level);

        public Task<LevelEntity> SetLevelTags(LevelEntity level, ICollection<int> tagIds);

        public Task CompleteLevel(LevelCompletedEntity completed);

        public Task UnCompleteLevel(LevelCompletedEntity completed);

        public Task FavoriteLevel(LevelFavoriteEntity favorite);

        public Task UnFavoriteLevel(LevelFavoriteEntity favorite);

        public Task<LevelFavoriteEntity?> GetFavoriteLevel(int levelId, int userId);

        public Task<LevelCompletedEntity?> GetCompletedLevel(int levelId, int userId);

        public Task CreateLevelVisit(LevelVisitEntity visit);
    }
}