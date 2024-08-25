using AutoMapper;
using Mouse.NET.Auth.Services;
using Mouse.NET.Data.Models;
using Mouse.NET.LevelComments.Data;
using Mouse.NET.LevelComments.Models;
using Mouse.Stick.Controllers.Auth;

namespace Mouse.NET.LevelComments.services;

public class LevelCommentService : ILevelCommentService
{
    
    private readonly IMapper mapper;
    private readonly IAuthService authService;
    private readonly ILevelCommentRepository levelCommentRepository;

    public LevelCommentService(IMapper mapper, ILevelCommentRepository levelCommentRepository, IAuthService authService) {
        this.levelCommentRepository = levelCommentRepository;
        this.mapper = mapper;
        this.authService = authService;
    }
    
    public async Task<ICollection<LevelComment>> GetLevelCommentCollection(int? levelId)
    {
        return mapper.Map<ICollection<LevelCommentEntity>, ICollection<LevelComment>>(await this.levelCommentRepository.GetLevelCommentCollection(levelId));
    }

    public async Task<LevelComment> GetLevelComment(int levelCommentId)
    {
        return mapper.Map<LevelCommentEntity, LevelComment>(await this.levelCommentRepository.GetLevelComment(levelCommentId));
    }

    public async Task<LevelComment> CreateLevelComment(LevelCommentCreateRequest request)
    {
        var comment = mapper.Map<LevelCommentCreateRequest, LevelCommentEntity>(request);
        comment.UserId = this.authService.GetAuthorizedUserId().GetValueOrDefault();
        return mapper.Map<LevelCommentEntity, LevelComment>(await this.levelCommentRepository.CreateLevelComment(comment));
    }

    public async Task<LevelComment> UpdateLevelComment(LevelCommentUpdateRequest request)
    {
        var commentExists = await this.levelCommentRepository.GetLevelComment(request.Id);
        if (commentExists.UserId != this.authService.GetAuthorizedUserId())
        {
            throw new BadHttpRequestException("Запрашиваемый комментарий не найден");
        }
        return mapper.Map<LevelCommentEntity, LevelComment>(await this.levelCommentRepository.UpdateLevelComment(mapper.Map(request, commentExists)));
    }

    public async Task<string> DeleteLevelComment(int levelCommentId)
    {
        var commentExists = await this.levelCommentRepository.GetLevelComment(levelCommentId);
        if (commentExists.UserId != this.authService.GetAuthorizedUserId())
        {
            throw new BadHttpRequestException("Запрашиваемый комментарий не найден");
        }
        await this.levelCommentRepository.DeleteLevelComment(commentExists);
        return "Ok";
    }
}