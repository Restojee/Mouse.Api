namespace Mouse.NET.Levels.dto;

public class LevelCompleteRequest
{
    public int LevelId { get; set; }   
    
    public int UserId { get; set; }
    
    public string? Description { get; set; }
}