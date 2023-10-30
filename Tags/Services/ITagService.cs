using Mouse.NET.Common;
using Mouse.NET.Tags.Models;

namespace Mouse.NET.Tags.services;

public interface ITagService
{
    public Task<ICollection<Tag>> GetTagCollection();

    public Task<Tag> GetTag(int tagId);

    public Task<Tag> CreateTag(TagCreateRequest createRequest);

    public Task<Tag> UpdateTag(TagUpdateRequest updateRequest);

    public Task<string> DeleteTag(int tagId);
}