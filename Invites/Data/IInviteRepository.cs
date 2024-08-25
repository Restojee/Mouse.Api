using Mouse.NET.Data.Models;

namespace Mouse.NET.Invites.Data;

public interface IInviteRepository
{
    public Task<InviteEntity> CreateInvite(string email, int userId);

    public Task UseInvite(string token);

    public Task<InviteEntity> GetWorkedInvite(string token);

    public Task<ICollection<InviteEntity>> GetInviteCollection();
}