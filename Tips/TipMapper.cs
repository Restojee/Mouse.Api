using AutoMapper;
using Mouse.NET.Data.Models;
using Mouse.NET.Tips.Models;

namespace Mouse.NET.Tips;

public class TipMapper : Profile
{
    public TipMapper()
    {
        CreateMap<TipEntity, Tip>();
        CreateMap<TipCreateRequest, TipEntity>();
        CreateMap<TipUpdateRequest, TipEntity>();
    }
}