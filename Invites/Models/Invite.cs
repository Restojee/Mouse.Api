using Mouse.NET.Common;

namespace Mouse.NET.Invites.Models;

public class Invite : Auditable
{
    public string Token { get; set; }
    
    public DateTime ExpirationDate { get; set; } 
}