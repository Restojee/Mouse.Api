using AutoMapper;
using Mouse.NET.Common;
using Mouse.NET.Data.Models;
using Mouse.NET.Levels.dto;
using Mouse.NET.Levels.Models;

namespace Mouse.NET.Levels;

public class LevelProfile : Profile
{
    public LevelProfile()
    {
        CreateMap<LevelEntity, Level>();
        CreateMap<Level, LevelEntity>();
        CreateMap<LevelCreateRequest, LevelEntity>();
        CreateMap<LevelNoteEntity, LevelNote>();
        CreateMap<LevelNote, LevelNoteEntity>();
        CreateMap<PagedResult<LevelEntity>, PagedResult<Level>>();
        CreateMap<LevelCompleteRequest, LevelCompletedEntity>();
        CreateMap<LevelFavoriteRequest, LevelFavoriteEntity>();
        CreateMap<LevelUnCompleteRequest, LevelCompletedEntity>();
        CreateMap<LevelUnFavoriteRequest, LevelFavoriteEntity>();
        CreateMap<LevelUpdateRequest, LevelEntity>();
        CreateMap<LevelImageUpdateRequest, LevelEntity>();
        CreateMap<LevelCompletedEntity, LevelCompleted>();
    }
}