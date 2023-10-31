using Mouse.NET.Common;
using Mouse.NET.Data.Models;

namespace Mouse.NET.Messages.Data
{
    public interface IMessageRepository
    {
        public Task<PagedResult<MessageEntity>> GetMessageCollection(PaginateRequest request);
        
        public Task<MessageEntity> GetMessage(int MessageId);
        
        public Task<MessageEntity> CreateMessage(MessageEntity Message);
        
        public Task<MessageEntity> UpdateMessage(MessageEntity Message);
        
        public Task DeleteMessage(MessageEntity Message);
    }
}