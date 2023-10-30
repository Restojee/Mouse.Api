using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Mouse.NET.Data.Models;

[Table("levels_notes")]
public class LevelNoteEntity : AuditableEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Column("level_id")]
    [ForeignKey("Level")]
    public int LevelId { get; set; }
    public LevelEntity Level { get; set; }

    [Column("user_id")]
    [ForeignKey("User")]
    public int UserId { get; set; }
    public UserEntity User { get; set; }

    [Column("text")]
    public string Text { get; set; }
}