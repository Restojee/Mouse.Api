using Mouse.NET.Common;
using Mouse.NET.Users.Models;

namespace Mouse.NET.Messages.Models;

public class Message : Auditable
{
    public int Id { get; set; }
    
    public string Text { get; set; }
    
    public User user { get; set; }
    
    public DateTime? CreatedUtcDate { get; set; } = DateTime.UtcNow;
    
    public DateTime? ModifiedUtcDate { get; set; } = DateTime.UtcNow;
}