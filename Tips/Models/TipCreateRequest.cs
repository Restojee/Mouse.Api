namespace Mouse.NET.Tips.Models;

public class TipCreateRequest
{
    public int UserId { get; set; }
    
    public string Text { get; set; }
    
    public string Title { get; set; }
}