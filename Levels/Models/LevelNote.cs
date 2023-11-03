using Mouse.NET.Common;
using Mouse.NET.Users.Models;

namespace Mouse.NET.Levels.Models;

public class LevelNote: Auditable
{
    public int Id { get; set; }
    
    public string Text { get; set; }
}