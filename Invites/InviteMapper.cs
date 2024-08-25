using AutoMapper;
using Mouse.NET.Data.Models;
using Mouse.NET.Invites.Models;

namespace Mouse.NET.Invites;

public class InviteMapper : Profile
{
    public InviteMapper()
    {
        CreateMap<InviteEntity, Invite>();
        CreateMap<Invite, InviteEntity>();
    }
}