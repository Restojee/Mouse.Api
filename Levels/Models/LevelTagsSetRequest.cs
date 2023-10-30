namespace Mouse.NET.Levels.Models;

public class LevelTagsSetRequest
{
    public int LevelId { get; set; }
    
    public ICollection<int> TagIds { get; set; }
}