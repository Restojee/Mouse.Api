﻿using AutoMapper;
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
    
    private ILevelRepository levelRepository;

    private IStorageService storageService;

    private IAuthService authService;

    public LevelService(IMapper mapper, ILevelRepository levelRepository, IStorageService storageService, IAuthService authService) {
        this.levelRepository = levelRepository;
        this.mapper = mapper;
        this.storageService = storageService;
        this.authService = authService;
    }
    
    public async Task<PagedResult<Level>> GetLevelCollection(LevelCollectionGetRequest request)
    {
        return mapper.Map<PagedResult<LevelEntity>, PagedResult<Level>>(
            await this.levelRepository.GetLevelCollection(request)
        );
    }
    
    public async Task<Level> GetLevelOrFail(int levelId)
    {
        var levelExists = await this.levelRepository.GetLevel(levelId);
        if (levelExists == null)
        {
            throw new BadHttpRequestException("Запрашиваемая карта не найдена");
        }
        return mapper.Map<LevelEntity, Level>(levelExists);
    }

    public async Task<Level> GetLevel(int levelId)
    {
        var level = mapper.Map<LevelEntity, Level>(await this.levelRepository.GetLevel(levelId));

        if (level != null)
        {
            await this.levelRepository.CreateLevelVisit(new LevelVisitEntity
            {
                LevelId = levelId,
                UserId = null
            });
        }
        
        return level;
    }

    public async Task<Level> CreateLevel(LevelCreateRequest request)
    {
        var level = mapper.Map<LevelCreateRequest, LevelEntity>(request);
        level.UserId = this.authService.GetAuthorizedUserId();
        return mapper.Map<LevelEntity, Level>(await this.levelRepository.CreateLevel(level));
    }

    public async Task<Level> UpdateLevel(LevelUpdateRequest request)
    {
        var levelExists = await this.levelRepository.GetLevel(request.Id);
        if (levelExists == null)
        {
            throw new BadHttpRequestException("Запрашиваемая карта не найдена");
        }
        return this.mapper.Map<LevelEntity, Level>(await this.levelRepository.UpdateLevel(this.mapper.Map(request, levelExists)));
    }

    public async Task<string> DeleteLevel(int levelId)
    {
        await this.levelRepository.DeleteLevel(this.mapper.Map<Level, LevelEntity>(await this.GetLevelOrFail(levelId)));
        return "Ok";
    }
    
    public async Task<Level> SetLevelTags(LevelTagsSetRequest request)
    {
        return mapper.Map<LevelEntity, Level>(
            await this.levelRepository.SetLevelTags(this.mapper.Map<Level, LevelEntity>(await this.GetLevelOrFail(request.LevelId)), request.TagIds)
        );
    }

    public async Task CompleteLevel(int levelId, IFormFile file)
    {
        var userId = this.authService.GetAuthorizedUserId();
        await this.GetLevelOrFail(levelId);
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
        await this.GetLevelOrFail(levelId);
        var levelCompletedExists = await this.levelRepository.GetCompletedLevel(levelId, this.authService.GetAuthorizedUserId());
        if (levelCompletedExists != null)
        {
            await this.levelRepository.UnCompleteLevel(levelCompletedExists);
        }
    }
    
    public async Task FavoriteLevel(int levelId)
    {
        var userId = this.authService.GetAuthorizedUserId();
        await this.GetLevelOrFail(levelId);
        var levelFavoriteExists = await this.levelRepository.GetFavoriteLevel(levelId, userId);
        if (levelFavoriteExists == null)
        {
            await this.levelRepository.FavoriteLevel(new LevelFavoriteEntity
            {
                LevelId = levelId,
                UserId = userId
            });
        }
    }
    
    public async Task UnFavoriteLevel(int levelId)
    {
        await this.GetLevelOrFail(levelId);
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