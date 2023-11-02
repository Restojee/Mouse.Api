using AutoMapper;
using Mouse.NET.Auth.Services;
using Mouse.NET.Common;
using Mouse.NET.Data.Models;
using Mouse.NET.Messages.Data;
using Mouse.NET.Messages.Models;
using Mouse.Stick.Controllers.Auth;

namespace Mouse.NET.Messages.services;

public class MessageService : IMessageService
{
    
    private readonly IMapper mapper;
    private readonly IAuthService authService;
    private readonly IMessageRepository messageRepository;

    public MessageService(IMapper mapper, IMessageRepository messageRepository, IAuthService authService) {
        this.messageRepository = messageRepository;
        this.mapper = mapper;
        this.authService = authService;
    }
    
    public async Task<PagedResult<Message>> GetMessageCollection(PaginateRequest request)
    {
        return mapper.Map<PagedResult<MessageEntity>, PagedResult<Message>>(await this.messageRepository.GetMessageCollection(request));
    }

    public async Task<Message> GetMessage(int messageId)
    {
        return mapper.Map<MessageEntity, Message>(await this.messageRepository.GetMessage(messageId));
    }

    public async Task<Message> CreateMessage(MessageCreateRequest request)
    {
        var comment = mapper.Map<MessageCreateRequest, MessageEntity>(request);
        comment.UserId = this.authService.GetAuthorizedUserId();
        return mapper.Map<MessageEntity, Message>(await this.messageRepository.CreateMessage(comment));
    }

    public async Task<Message> UpdateMessage(MessageUpdateRequest request)
    {
        var commentExists = await this.messageRepository.GetMessage(request.Id);
        if (commentExists.UserId != this.authService.GetAuthorizedUserId())
        {
            throw new BadHttpRequestException("Запрашиваемый комментарий не найден");
        }
        return mapper.Map<MessageEntity, Message>(await this.messageRepository.UpdateMessage(mapper.Map(request, commentExists)));
    }

    public async Task<string> DeleteMessage(int messageId)
    {
        var commentExists = await this.messageRepository.GetMessage(messageId);
        if (commentExists.UserId != this.authService.GetAuthorizedUserId())
        {
            throw new BadHttpRequestException("Запрашиваемый комментарий не найден");
        }
        await this.messageRepository.DeleteMessage(commentExists);
        return "Ok";
    }
}