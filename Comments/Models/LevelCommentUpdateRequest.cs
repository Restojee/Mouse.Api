namespace Mouse.NET.LevelComments.Models;

public class LevelCommentUpdateRequest
{
    public int Id { get; set; }
    
    public int UserId { get; set; }
    
    public int Text { get; set; }
}