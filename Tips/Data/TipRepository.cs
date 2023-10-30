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
        return await PaginationExtensions.ToPagedResult(this.context.Tips.Include(tip => tip.User), request.Page, request.Size);
    }
    
    public async Task<TipEntity?> GetTip(int levelId)
    { 
       return await this.context.Tips.FirstOrDefaultAsync(level => level.Id.Equals(levelId));
    }

    public async Task<TipEntity?> CreateTip(TipEntity level)
    {
        await this.context.Tips.AddAsync(level);
        await this.context.SaveChangesAsync();
        
        return await this.GetTip(level.Id);
    }

    public async Task<TipEntity?> UpdateTip(TipEntity level)
    {
        this.context.Entry(level).State = EntityState.Modified;
        await this.context.SaveChangesAsync();
        
        return await this.GetTip(level.Id);
    }

    public async Task DeleteTip(TipEntity level)
    {
        this.context.Tips.Remove(level);
        await this.context.SaveChangesAsync();
    }

    public async Task<ICollection<TipEntity>> GetTipsById(ICollection<int> tipIds)
    {
        return await this.context.Tips.Where(tip => tipIds.Contains(tip.Id)).ToListAsync();
    }

}