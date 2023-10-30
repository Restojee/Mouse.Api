using AutoMapper;
using Mouse.NET.Data.Models;
using Mouse.NET.Tags.Models;

namespace Mouse.NET.Tags;

public class TagMapper : Profile
{
    public TagMapper()
    {
        CreateMap<TagEntity, Tag>();
        CreateMap<TagCreateRequest, TagEntity>();
        CreateMap<TagUpdateRequest, TagEntity>();
        CreateMap<Tag, TagEntity>();
    }
}