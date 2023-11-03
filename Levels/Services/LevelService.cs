using AutoMapper;
using Mouse.NET.Auth.Services;
using Mouse.NET.Common;
using Mouse.NET.Data.Models;
using Mouse.NET.Levels.Data;
using Mouse.NET.Levels.dto;
using Mouse.NET.Levels.Models;
using Mouse.NET.Storage;
using Mouse.Stick.Controllers.Auth;

namespace Mouse.NET.Levels.services;

public class LevelService : ILevelService
{
    
    private readonly IMapper mapper;
    private readonly ILevelRepository levelRepository;
    private readonly IStorageService storageService;
    private readonly IAuthService authService;

    public LevelService(IMapper mapper, ILevelRepository levelRepository, IStorageService storageService, IAuthService authService) {
        this.levelRepository = levelRepository;
        this.mapper = mapper;
        this.storageService = storageService;
        this.authService = authService;
    }
    
    public async Task<PagedResult<Level>> GetLevelCollection(LevelCollectionGetRequest request)
    {
        return mapper.Map<PagedResult<LevelEntity>, PagedResult<Level>>(await this.levelRepository.GetLevelCollection(request));
    }

    public async Task<Level> GetLevel(int levelId)
    {
        var userId = this.authService.GetAuthorizedUserId();
        var levelExists = mapper.Map<LevelEntity, Level>(await this.levelRepository.GetLevel(levelId, userId));
        if (levelExists == null)
        {
            throw new BadHttpRequestException("Запрашиваемая карта не найдена");
        }
        await this.levelRepository.CreateLevelVisit(new LevelVisitEntity { LevelId = levelId, UserId = userId });
        return levelExists;
    }

    public async Task<Level> CreateLevel(LevelCreateRequest request)
    {
        var level = mapper.Map<LevelCreateRequest, LevelEntity>(request);
        level.UserId = this.authService.GetAuthorizedUserId();
        return mapper.Map<LevelEntity, Level>(await this.levelRepository.CreateLevel(level));
    }

    public async Task<Level> UpdateLevel(LevelUpdateRequest request)
    {
        var levelExists = await this.levelRepository.GetLevel(request.Id, this.authService.GetAuthorizedUserId());
        if (levelExists == null)
        {
            throw new BadHttpRequestException("Запрашиваемая карта не найдена");
        }
        return this.mapper.Map<LevelEntity, Level>(await this.levelRepository.UpdateLevel(this.mapper.Map(request, levelExists)));
    }

    public async Task<string> DeleteLevel(int levelId)
    {
        var levelExists = await this.levelRepository.GetLevel(levelId);
        if (levelExists == null)
        {
            throw new BadHttpRequestException("Запрашиваемая карта не найдена");
        }
        await this.levelRepository.DeleteLevel(levelExists);
        return "Ok";
    }
    
    public async Task<Level> SetLevelTags(LevelTagsSetRequest request)
    {
        var levelExists = await this.levelRepository.GetLevel(request.LevelId);
        if (levelExists == null)
        {
            throw new BadHttpRequestException("Запрашиваемая карта не найдена");
        }
        return mapper.Map<LevelEntity, Level>(await this.levelRepository.SetLevelTags(levelExists, request.TagIds));
    }

    public async Task<Level> SetLevelNote(LevelNoteSetRequest request)
    {
        var levelExists = await this.levelRepository.GetLevel(request.LevelId);
        if (levelExists == null)
        {
            throw new BadHttpRequestException("Запрашиваемая карта не найдена");
        }
        var userId = this.authService.GetAuthorizedUserId();
        var levelNoteExists = await this.levelRepository.GetLevelNote(request.LevelId, userId);
        if (levelNoteExists == null)
        {
            await this.levelRepository.CreateLevelNote(new LevelNoteEntity
            {
                UserId = userId,
                LevelId = request.LevelId,
                Text = request.Text
            });
        }
        else
        {
            levelNoteExists.Text = request.Text;
            await this.levelRepository.UpdateLevelNote(levelNoteExists);
        }
        return await this.GetLevel(request.LevelId);
    }

    public async Task CompleteLevel(int levelId, IFormFile file)
    {
        var userId = this.authService.GetAuthorizedUserId();
        var levelExists = await this.levelRepository.GetLevel(levelId);
        if (levelExists == null)
        {
            throw new BadHttpRequestException("Запрашиваемая карта не найдена");
        }
        var levelCompletedExists = await this.levelRepository.GetCompletedLevel(levelId, userId);
        if (levelCompletedExists != null)
        {
            await this.levelRepository.UnCompleteLevel(levelCompletedExists);
        }
        await this.levelRepository.CompleteLevel(new LevelCompletedEntity
        {
            LevelId = levelId,
            UserId = userId,
            Image = await this.storageService.Upload(file)
        });
    }
    
    public async Task UnCompleteLevel(int levelId)
    {
        var levelExists = await this.levelRepository.GetLevel(levelId);
        if (levelExists == null)
        {
            throw new BadHttpRequestException("Запрашиваемая карта не найдена");
        }
        var levelCompletedExists = await this.levelRepository.GetCompletedLevel(levelId, this.authService.GetAuthorizedUserId());
        if (levelCompletedExists != null)
        {
            await this.levelRepository.UnCompleteLevel(levelCompletedExists);
        }
    }
    
    public async Task FavoriteLevel(int levelId)
    {
        var userId = this.authService.GetAuthorizedUserId();
        var levelExists = await this.levelRepository.GetLevel(levelId);
        if (levelExists == null)
        {
            throw new BadHttpRequestException("Запрашиваемая карта не найдена");
        }
        if (await this.levelRepository.GetFavoriteLevel(levelId, userId) != null)
        {
            return;
        }
        await this.levelRepository.FavoriteLevel(new LevelFavoriteEntity
        {
            LevelId = levelId,
            UserId = userId
        });
    }
    
    public async Task UnFavoriteLevel(int levelId)
    {
        if (await this.levelRepository.GetLevel(levelId) == null)
        {
            throw new BadHttpRequestException("Запрашиваемая карта не найдена");
        }
        var levelFavoriteExists = await this.levelRepository.GetFavoriteLevel(levelId, this.authService.GetAuthorizedUserId());
        if (levelFavoriteExists != null)
        {
            await this.levelRepository.UnFavoriteLevel(levelFavoriteExists);
        }
    }
    
    public async Task UpdateLevelImage(int levelId, IFormFile file)
    {
        var levelExists = await this.levelRepository.GetLevel(levelId);
        if (levelExists == null)
        {
            throw new BadHttpRequestException("Запрашиваемая карта не найдена");
        }
        levelExists.Image = await this.storageService.Upload(file);
        await this.levelRepository.UpdateLevel(levelExists);
    }
}