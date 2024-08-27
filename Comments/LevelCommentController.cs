using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mouse.NET.LevelComments.Models;
using Mouse.NET.LevelComments.services;

namespace Mouse.NET.LevelComments;

[ApiController]
[Route("comments")]
public class LevelCommentController : ControllerBase
{
    
    private readonly ILevelCommentService levelCommentService;

    public LevelCommentController(ILevelCommentService levelCommentService)
    {
        this.levelCommentService = levelCommentService;
    }

    [HttpGet("collect")]
    public async Task<ICollection<LevelComment>> GetLevelCommentCollection([FromQuery] LevelCommentCollectRequest request)
    {
        return await this.levelCommentService.GetLevelCommentCollection(request.levelId, request.userId);
    }

    [HttpGet("by-one/{levelCommentId}")]
    public async Task<LevelComment> GetLevelComment([FromRoute] int levelCommentId)
    {
        return await this.levelCommentService.GetLevelComment(levelCommentId);
    }
    
    [HttpPut("update")]
    [Authorize]
    public async Task<LevelComment> UpdateLevelComment([FromBody] LevelCommentUpdateRequest updateRequest)
    {
        return await this.levelCommentService.UpdateLevelComment(updateRequest);
    }
    
    [HttpPost("create")]
    [Authorize]
    public async Task<LevelComment> CreateLevelComment([FromBody] LevelCommentCreateRequest createRequest)
    {
        return await this.levelCommentService.CreateLevelComment(createRequest);
    }
    
    [HttpDelete("remove/{levelCommentId}")]
    [Authorize]
    public async Task<string> DeleteLevelComments([FromRoute] int levelCommentId)
    {
        return await this.levelCommentService.DeleteLevelComment(levelCommentId);
    }
}