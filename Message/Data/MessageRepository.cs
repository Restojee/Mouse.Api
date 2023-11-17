using Microsoft.EntityFrameworkCore;
using Mouse.NET.Common;
using Mouse.NET.Data;
using Mouse.NET.Data.Models;
using Mouse.NET.Messages.Models;

namespace Mouse.NET.Messages.Data;

public class MessageRepository : IMessageRepository
{
    private readonly MouseDbContext context;
    
    public MessageRepository(MouseDbContext context)
    {
        this.context = context;
    }
    
    public async Task<MessageEntity?> GetMessage(int levelId)
    { 
       return await this.context.Messages
           .Include(message => message.User)
           .FirstOrDefaultAsync(level => level.Id.Equals(levelId));
    }

    public async Task<MessageEntity?> CreateMessage(MessageEntity level)
    {
        await this.context.Messages.AddAsync(level);
        await this.context.SaveChangesAsync();
        
        return await this.GetMessage(level.Id);
    }

    public async Task<MessageEntity?> UpdateMessage(MessageEntity level)
    {
        this.context.Entry(level).State = EntityState.Modified;
        await this.context.SaveChangesAsync();
        
        return await this.GetMessage(level.Id);
    }

    public async Task DeleteMessage(MessageEntity level)
    {
        this.context.Messages.Remove(level);
        await this.context.SaveChangesAsync();
    }

    public async Task<PagedResult<MessageEntity>> GetMessageCollection(PaginateRequest request)
    {
        return await PaginationExtensions.ToPagedResult(this.context.Messages
            .Include(message => message.User)
            .OrderByDescending(level => level.CreatedUtcDate)
            .AsQueryable(),
            request.Page,
            request.Size
        );
    }

}