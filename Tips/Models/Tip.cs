using Mouse.NET.Common;
using Mouse.NET.Users.Models;

namespace Mouse.NET.Tips.Models;

public class Tip : Auditable
{
    public int Id { get; set; }
    
    public string Title { get; set; }
    
    public string Text { get; set; }
    
    public User User { get; set; }
}