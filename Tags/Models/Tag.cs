using Mouse.NET.Common;

namespace Mouse.NET.Tags.Models;

public class Tag : Auditable
{
    public int Id { get; set; }
    
    public string Name { get; set; }
    
    public string Description { get; set; }
}