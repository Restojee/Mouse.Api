using Microsoft.EntityFrameworkCore;
using Mouse.NET.Common;
using Mouse.NET.Data;
using Mouse.NET.Data.Models;

namespace Mouse.NET.Tips.Data;

public class TipRepository : ITipRepository
{
    private readonly MouseDbContext context;
    
    public TipRepository(MouseDbContext context)
    {
        this.context = context;
    }
    
    public async Task<PagedResult<TipEntity>> GetTipCollection(PaginateRequest request)
    {
        return await PaginationExtensions.ToPagedResult(
            this.context.Tips
                .Include(tip => tip.User)
                .OrderBy(tip => tip.CreatedUtcDate)
                .AsQueryable(),
            request.Page, request.Size);
    }
    
    public async Task<TipEntity?> GetTip(int tipId)
    { 
       return await this.context.Tips.Include(tip => tip.User).FirstOrDefaultAsync(level => level.Id.Equals(tipId));
    }

    public async Task<TipEntity?> CreateTip(TipEntity tip)
    {
        await this.context.Tips.AddAsync(tip);
        await this.context.SaveChangesAsync();
        
        return await this.GetTip(tip.Id);
    }

    public async Task<TipEntity?> UpdateTip(TipEntity tip)
    {
        this.context.Entry(tip).State = EntityState.Modified;
        await this.context.SaveChangesAsync();
        
        return await this.GetTip(tip.Id);
    }

    public async Task DeleteTip(TipEntity tip)
    {
        this.context.Tips.Remove(tip);
        await this.context.SaveChangesAsync();
    }

    public async Task<ICollection<TipEntity>> GetTipsById(ICollection<int> tipIds)
    {
        return await this.context.Tips.Where(tip => tipIds.Contains(tip.Id)).ToListAsync();
    }

}