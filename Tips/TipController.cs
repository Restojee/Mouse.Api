using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mouse.NET.Common;
using Mouse.NET.Tips.Models;
using Mouse.NET.Tips.services;

namespace Mouse.NET.Tips;

[ApiController]
[Route("tips")]
public class TipController : ControllerBase
{
    
    private readonly ITipService tagService;

    public TipController(ITipService tagService)
    {
        this.tagService = tagService;
    }

    [HttpGet("collect")]
    [Authorize]
    public async Task<PagedResult<Tip>> GetTipCollection(PaginateRequest request)
    {
        return await this.tagService.GetTipCollection(request);
    }

    [HttpGet("{tagId}")]
    public async Task<Tip> GetTip([FromRoute] int tagId)
    {
        return await this.tagService.GetTip(tagId);
    }
    
    [HttpPut]
    [Authorize("update")]
    public async Task<Tip> UpdateTip([FromBody] TipUpdateRequest updateRequest)
    {
        return await this.tagService.UpdateTip(updateRequest);
    }
    
    [HttpPost("create")]
    [Authorize]
    public async Task<Tip> CreateTip([FromBody] TipCreateRequest createRequest)
    {
        return await this.tagService.CreateTip(createRequest);
    }
    
    [HttpDelete("delete/{tagId}")]
    [Authorize]
    public async Task<string> DeleteTips([FromRoute] int tagId)
    {
        return await this.tagService.DeleteTip(tagId);
    }
}