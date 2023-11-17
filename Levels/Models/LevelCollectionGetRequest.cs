using Mouse.NET.Common;

namespace Mouse.NET.Levels.Models;

public class LevelCollectionGetRequest : PaginateRequest
{
    public bool? IsCompleted { get; set; }

    public bool? IsFavorite { get; set; }
    
    public bool? HasNote { get; set; }
    
    public string? Name { get; set; }
    
    public long[]? TagIds { get; set; }
    
    public int? UserId { get; set; }

    public bool? IsCreatedByUser { get; set; }
}