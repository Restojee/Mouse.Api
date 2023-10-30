﻿using Mouse.NET.Common;
using Mouse.NET.Users.Models;

namespace Mouse.NET.Levels.dto;

public class LevelCompleted: Auditable

{
    public User User { get; set; }
    
    public string Image { get; set; }
    
    public DateTime CreatedUtcDate { get; set; }
    
    public DateTime ModifiedUtcDate { get; set; }
}