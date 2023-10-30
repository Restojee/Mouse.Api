using Mouse.NET.Common;
using Mouse.NET.Users.Models;

namespace Mouse.NET.LevelComments.Models;

public class LevelComment : Auditable
{
    public int Id { get; set; }
    
    public string Text { get; set; }
    
    public User user { get; set; }
}