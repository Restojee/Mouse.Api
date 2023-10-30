using Mouse.NET.Common;
using Mouse.NET.Data.Models;
using Mouse.NET.Tips.Models;

namespace Mouse.NET.Tips.Data
{
    public interface ITipRepository
    {
        public Task<PagedResult<TipEntity>> GetTipCollection(PaginateRequest request);
        
        public Task<TipEntity> GetTip(int tipId);
        
        public Task<TipEntity> CreateTip(TipEntity tip);
        
        public Task<TipEntity> UpdateTip(TipEntity tip);
        
        public Task DeleteTip(TipEntity tip);
    }
}