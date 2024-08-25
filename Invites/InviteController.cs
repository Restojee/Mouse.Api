using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mouse.NET.Invites.Models;

namespace Mouse.NET.Invites;

[ApiController]
[Route("api/invites")]
public class InviteController : ControllerBase
{
    private readonly IInviteService inviteService;

    public InviteController(IInviteService inviteService)
    {
        this.inviteService = inviteService;
    }
    
    [HttpPost("create")]
    [Authorize]
    public async Task<Invite> CreateInvitation([FromBody] InviteCreateRequest inviteCreateRequest)
    {
        return await this.inviteService.CreateInvite(inviteCreateRequest.Email);
    }
    
    [HttpGet("collect")]
    [Authorize]
    public async Task<ICollection<Invite>> GetInvites()
    {
        return await this.inviteService.GetInviteCollection();
    }
}