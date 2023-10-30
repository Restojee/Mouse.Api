using AutoMapper;
using Mouse.NET.Data.Models;
using Mouse.NET.LevelComments.Models;

namespace Mouse.NET.LevelComments;

public class LevelCommentMapper : Profile
{
    public LevelCommentMapper()
    {
        CreateMap<LevelCommentEntity, LevelComment>();
        CreateMap<LevelCommentCreateRequest, LevelCommentEntity>();
        CreateMap<LevelCommentUpdateRequest, LevelCommentEntity>();
    }
}