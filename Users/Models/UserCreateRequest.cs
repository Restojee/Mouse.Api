namespace Mouse.NET.Users.Models;

public class UserCreateRequest
{
    public string UserName { get; set; }
    
    public string Password { get; set; }
    
    public string? Avatar { get; set; }
}