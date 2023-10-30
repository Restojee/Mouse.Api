using Mouse.NET.Common;

namespace Mouse.NET.Users.Models;

public class User : Auditable
{
    public int Id { get; set; }
    
    public string Avatar { get; set; }
    
    public string Username { get; set; }
}
