using Microsoft.EntityFrameworkCore;
using Mouse.NET.Data;
using Mouse.NET.Data.Models;

namespace Mouse.NET.Invites.Data;

public class InviteRepository : IInviteRepository
{
    private readonly MouseDbContext context;

    public InviteRepository(MouseDbContext context)
    {
        this.context = context;
    }

    public async Task<InviteEntity> CreateInvite(string email, int userId)
    {
        var token = InviteRepository.GenerateFromGuid(7);
        var invite = new InviteEntity
        {
            UserId = userId,
            Email = email,
            Token = token,
            ExpirationDate = DateTime.UtcNow.AddDays(7),
            IsUsed = false
        };

        this.context.Invites.Add(invite);
        await this.context.SaveChangesAsync();

        return invite;
    }

    public async Task<InviteEntity?> GetWorkedInvite(string token)
    {
        var invite = await this.context.Invites
            .FirstOrDefaultAsync(i => i.Token == token && i.ExpirationDate > DateTime.UtcNow && !i.IsUsed);

        if (invite != null && !invite.IsUsed)
        {
            return invite;
        }

        return null;
    }
    
    public async Task<ICollection<InviteEntity>> GetInviteCollection()
    {
        return await this.context.Invites.ToListAsync();
    }

    public async Task UseInvite(string token)
    {
        var invitation = await this.context.Invites
            .FirstOrDefaultAsync(i => i.Token == token);

        if (invitation != null)
        {
            invitation.IsUsed = true;
            await this.context.SaveChangesAsync();
        }
    }
    
    private static string GenerateFromGuid(int length = 5)
    {
        if (length <= 0 || length > 32)
        {
            throw new ArgumentException("Длина должна быть между 1 и 32.", nameof(length));
        }
        
        string guidString = Guid.NewGuid().ToString("N");
        
        return guidString.Substring(0, length);
    }
}