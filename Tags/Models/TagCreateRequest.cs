namespace Mouse.NET.Tags.Models;

public class TagCreateRequest
{
    public string Name { get; set; }
    
    public string Description { get; set; }
    
    public int UserId { get; set; }
}