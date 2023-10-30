using Mouse.NET.Common;
using Mouse.NET.Data.Models;
using Mouse.NET.Tags.Models;

namespace Mouse.NET.Tags.Data
{
    public interface ITagRepository
    {
        public Task<ICollection<TagEntity>> GetTagCollection();
        
        public Task<TagEntity> GetTag(int tagId);
        
        public Task<TagEntity> GetTagByName(string name);
        
        public Task<TagEntity> CreateTag(TagEntity tag);
        
        public Task<TagEntity> UpdateTag(TagEntity tag);
        
        public Task DeleteTag(TagEntity tag);
    }
}