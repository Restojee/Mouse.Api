using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mouse.NET.Tags.Models;
using Mouse.NET.Tags.services;

namespace Mouse.NET.Tags;

[ApiController]
[Route("tags")]
public class TagController : ControllerBase
{
    
    private readonly ITagService tagService;

    public TagController(ITagService tagService)
    {
        this.tagService = tagService;
    }

    [HttpGet("collect")]
    public async Task<ICollection<Tag>> GetTagCollection()
    {
        return await this.tagService.GetTagCollection();
    }

    [HttpGet("by-id/{tagId}")]
    public async Task<Tag> GetTag([FromRoute] int tagId)
    {
        return await this.tagService.GetTag(tagId);
    }
    
    [HttpPut("update")]
    [Authorize]
    public async Task<Tag> UpdateTag([FromBody] TagUpdateRequest updateRequest)
    {
        return await this.tagService.UpdateTag(updateRequest);
    }
    
    [HttpPost("create")]
    [Authorize]
    public async Task<Tag> CreateTag([FromBody] TagCreateRequest createRequest)
    {
        return await this.tagService.CreateTag(createRequest);
    }
    
    [HttpDelete("delete/{tagId}")]
    [Authorize]
    public async Task<string> DeleteTags([FromRoute] int tagId)
    {
        return await this.tagService.DeleteTag(tagId);
    }
}