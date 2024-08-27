using Mouse.NET.Common;
using Mouse.NET.Data.Models;
using Mouse.NET.LevelComments.Models;

namespace Mouse.NET.LevelComments.Data
{
    public interface ILevelCommentRepository
    {
        public Task<ICollection<LevelCommentEntity>> GetLevelCommentCollection(int? levelId, int? userId);
        
        public Task<LevelCommentEntity> GetLevelComment(int levelCommentId);
        
        public Task<LevelCommentEntity> CreateLevelComment(LevelCommentEntity levelComment);
        
        public Task<LevelCommentEntity> UpdateLevelComment(LevelCommentEntity levelComment);
        
        public Task DeleteLevelComment(LevelCommentEntity levelComment);
    }
}