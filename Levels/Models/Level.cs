using Mouse.NET.Common;
using Mouse.NET.Tags.Models;
using Mouse.NET.Users.Models;

namespace Mouse.NET.Levels.Models;

public class Level: Auditable
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public string Image { get; set; }
    
    public User User { get; set; }
    
    public ICollection<Tag> Tags { get; set; }
    
    public ICollection<LevelNote> Notes { get; set; }
    
    public int CommentsCount { get; set; }
    
    public int CompletedCount { get; set; }
    
    public int FavoritesCount { get; set; }
    
    public int VisitsCount { get; set; }
    
    public bool IsCompletedByUser { get; set; }

    public bool IsFavoriteByUser { get; set; }
}