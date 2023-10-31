using Mouse.NET.Common;
using Mouse.NET.Messages.Models;

namespace Mouse.NET.Messages.services;

public interface IMessageService
{
    public Task<PagedResult<Message>> GetMessageCollection(PaginateRequest request);

    public Task<Message> GetMessage(int messageId);

    public Task<Message> CreateMessage(MessageCreateRequest createRequest);

    public Task<Message> UpdateMessage(MessageUpdateRequest updateRequest);

    public Task<string> DeleteMessage(int messageId);
}