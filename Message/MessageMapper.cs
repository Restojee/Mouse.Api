using AutoMapper;
using Mouse.NET.Common;
using Mouse.NET.Data.Models;
using Mouse.NET.Messages.Models;

namespace Mouse.NET.Messages;

public class MessageMapper : Profile
{
    public MessageMapper()
    {
        CreateMap<MessageEntity, Message>();
        CreateMap<MessageCreateRequest, MessageEntity>();
        CreateMap<MessageUpdateRequest, MessageEntity>();
        CreateMap<PagedResult<MessageEntity>, PagedResult<Message>>();
    }
}