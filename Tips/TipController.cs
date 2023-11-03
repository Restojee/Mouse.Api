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
    
    private readonly ITipService tipService;

    public TipController(ITipService tipService)
    {
        this.tipService = tipService;
    }

    [Authorize]
    [HttpGet("collect")]
    public async Task<PagedResult<Tip>> GetTipCollection([FromQuery] PaginateRequest request)
    {
        return await this.tipService.GetTipCollection(request);
    }

    [HttpGet("{tipId}")]
    public async Task<Tip> GetTip([FromRoute] int tipId)
    {
        return await this.tipService.GetTip(tipId);
    }
    
    [Authorize]
    [HttpPut("update")]
    public async Task<Tip> UpdateTip([FromBody] TipUpdateRequest updateRequest)
    {
        return await this.tipService.UpdateTip(updateRequest);
    }
    
    [Authorize]
    [HttpPost("create")]
    public async Task<Tip> CreateTip([FromBody] TipCreateRequest createRequest)
    {
        return await this.tipService.CreateTip(createRequest);
    }
    
    [HttpDelete("delete/{tipId}")]
    [Authorize]
    public async Task<string> DeleteTips([FromRoute] int tipId)
    {
        return await this.tipService.DeleteTip(tipId);
    }
}