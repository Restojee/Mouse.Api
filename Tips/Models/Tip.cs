using Mouse.NET.Common;

namespace Mouse.NET.Tips.Models;

public class Tip : Auditable
{
    public int Id { get; set; }
    
    public string Title { get; set; }
    
    public string Text { get; set; }
}