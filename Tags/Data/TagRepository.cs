using Microsoft.EntityFrameworkCore;
using Mouse.NET.Data;
using Mouse.NET.Data.Models;

namespace Mouse.NET.Tags.Data;

public class TagRepository : ITagRepository
{
    private readonly MouseDbContext context;
    
    public TagRepository(MouseDbContext context)
    {
        this.context = context;
    }
    
    public async Task<ICollection<TagEntity>> GetTagCollection()
    {
        return await this.context.Tags.ToListAsync();
    }
    
    public async Task<TagEntity?> GetTag(int tagId)
    { 
       return await this.context.Tags
           .AsNoTracking()
           .Include(tag => tag.User)
           .FirstOrDefaultAsync(tag => tag.Id.Equals(tagId));
    }

    public async Task<TagEntity?> GetTagByName(string name)
    { 
        return await this.context.Tags.FirstOrDefaultAsync(tag => tag.Name.Equals(name));
    }
    
    public async Task<TagEntity?> CreateTag(TagEntity tag)
    {
        await this.context.Tags.AddAsync(tag);
        await this.context.SaveChangesAsync();
        
        return await this.GetTag(tag.Id);
    }

    public async Task<TagEntity?> UpdateTag(TagEntity tag)
    {
        this.context.Entry(tag).State = EntityState.Modified;
        this.context.Entry(tag).Property(x => x.Name).IsModified = true;
        this.context.Entry(tag).Property(x => x.Description).IsModified = true;
        await this.context.SaveChangesAsync();
        
        return await this.GetTag(tag.Id);
    }

    public async Task DeleteTag(TagEntity tag)
    {
        this.context.Tags.Remove(tag);
        await this.context.SaveChangesAsync();
    }
}