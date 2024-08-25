using Mouse.NET.Invites.Models;

public interface IInviteService
{
    public Task<Invite> CreateInvite(string email);
    
    public Task<Invite> GetInvite(string token);

    public Task UseInvite(string token);

    public Task<ICollection<Invite>> GetInviteCollection();
}