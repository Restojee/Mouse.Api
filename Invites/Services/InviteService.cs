using AutoMapper;
using Mouse.NET.Data.Models;
using Mouse.NET.Invites.Data;
using Mouse.NET.Invites.Models;
using Mouse.Stick.Controllers.Auth;

namespace Mouse.NET.Invites.Services;

public class InviteService : IInviteService
{
    private readonly IInviteRepository inviteRepository;
    private readonly IMapper mapper;
    private readonly IAuthService authService;

    public InviteService(IInviteRepository inviteRepository, IMapper mapper, IAuthService authService)
    {
        this.inviteRepository = inviteRepository;
        this.mapper = mapper;
        this.authService = authService;
    }
    
    public async Task<Invite> CreateInvite(string email)
    {
        return this.mapper.Map<InviteEntity, Invite>(await this.inviteRepository.CreateInvite(email, 
            this.authService.GetAuthorizedUserId().GetValueOrDefault()));
    }
    
    public async Task<Invite> GetInvite(string token)
    {
        var invite = await this.inviteRepository.GetWorkedInvite(token);
        return new Invite
        {
            Token = invite.Token,
            ModifiedUtcDate = invite.ModifiedUtcDate,
            CreatedUtcDate = invite.CreatedUtcDate,
            ExpirationDate = invite.ExpirationDate
        };
    }

    public async Task<ICollection<Invite>> GetInviteCollection()
    {
        return this.mapper.Map<ICollection<InviteEntity>, ICollection<Invite>>(await this.inviteRepository.GetInviteCollection());
    }
    
    public async Task UseInvite(string token)
    {
        await this.inviteRepository.UseInvite(token);
    }
}