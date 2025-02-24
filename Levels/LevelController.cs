using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mouse.NET.Common;
using Mouse.NET.Levels.dto;
using Mouse.NET.Levels.Models;
using Mouse.NET.Levels.services;

namespace Mouse.NET.Levels;

[ApiController]
[Route("levels")]
public class LevelController : ControllerBase
{
    
    private readonly ILevelService levelService;

    public LevelController(ILevelService levelService)
    {
        this.levelService = levelService;
    }

    [HttpGet("collect")]
    public async Task<PagedResult<Level>> GetLevelCollection([FromQuery] LevelCollectionGetRequest getRequest)
    {
        return await this.levelService.GetLevelCollection(getRequest);
    }

    [HttpGet("by-id/{levelId}")]
    public async Task<Level> GetLevel([FromRoute] int levelId)
    {
        return await this.levelService.GetLevel(levelId);
    }
    
    [HttpPut("update")]
    [Authorize]
    public async Task<Level> UpdateLevel([FromBody] LevelUpdateRequest updateRequest)
    {
        return await this.levelService.UpdateLevel(updateRequest);
    }
    
    [HttpPost("create")]
    [Authorize]
    public async Task<Level> CreateLevel([FromBody] LevelCreateRequest createRequest)
    {
        return await this.levelService.CreateLevel(createRequest);
    }
    
    [HttpPost("{levelId}/update-image")]
    [Authorize]
    public async Task<string> UpdateLevelImage([FromRoute] int levelId, IFormFile formFile)
    {
        await this.levelService.UpdateLevelImage(levelId, formFile);
        return "Ok";
    }
    
    [HttpDelete("remove/{levelId}")]
    [Authorize]
    public async Task<string> DeleteLevel([FromRoute] int levelId)
    {
        return await this.levelService.DeleteLevel(levelId);
    }
    
    [HttpPost("{levelId}/completed/create")]
    [Authorize]
    public async Task<string> CompleteLevel([FromRoute] int levelId, IFormFile formFile)
    {
        await this.levelService.CompleteLevel(levelId, formFile, "");
        return "Ok";
    }
    
    [HttpDelete("{levelId}/completed/{completedId}/remove")]
    [Authorize]
    public async Task<string> UnCompleteLevel([FromRoute] int completedId)
    {
        await this.levelService.UnCompleteLevel(completedId);
        return "Ok";
    }
    
    [HttpPost("{levelId}/favorites/create")]
    [Authorize]
    public async Task<string> FavoriteLevel([FromRoute] int levelId)
    {
        await this.levelService.FavoriteLevel(levelId);
        return "Ok";
    }
    
    [HttpDelete("{levelId}/favorites/delete")]
    [Authorize]
    public async Task<string> UnFavoriteLevel([FromRoute] int levelId)
    {
        await this.levelService.UnFavoriteLevel(levelId);
        return "Ok";
    }
    
    [HttpPut("set-tags")]
    [Authorize]
    public async Task<Level> SetLevelTags([FromBody] LevelTagsSetRequest request)
    {
        return await this.levelService.SetLevelTags(request);
    }
    
    [HttpPut("set-note")]
    [Authorize]
    public async Task<Level> SetLevelNote([FromBody] LevelNoteSetRequest request)
    {
        return await this.levelService.SetLevelNote(request);
    }
}