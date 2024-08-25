using Mouse.NET.Common;

namespace Mouse.NET.Users.Models;

public class User : Auditable
{
    public int Id { get; set; }
    
    public string Avatar { get; set; }
    
    public string Username { get; set; }
    
    public string? Email { get; set; }
    
    public int LevelsCount { get; set; }
    
    public int CompletedCount { get; set; }
    
    public int CommentsCount { get; set; }
    
    public int FavoritesCount { get; set; }
}
