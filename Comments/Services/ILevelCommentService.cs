using Mouse.NET.Common;
using Mouse.NET.LevelComments.Models;

namespace Mouse.NET.LevelComments.services;

public interface ILevelCommentService
{
    public Task<ICollection<LevelComment>> GetLevelCommentCollection(int? levelId);

    public Task<LevelComment> GetLevelComment(int levelCommentId);

    public Task<LevelComment> CreateLevelComment(LevelCommentCreateRequest createRequest);

    public Task<LevelComment> UpdateLevelComment(LevelCommentUpdateRequest updateRequest);

    public Task<string> DeleteLevelComment(int levelCommentId);
}