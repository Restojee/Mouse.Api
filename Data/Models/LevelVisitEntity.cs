using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mouse.NET.Data.Models;

[Table("level_visits")]
public class LevelVisitEntity : AuditableEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column("level_id")]
    [ForeignKey("Level")]
    public int LevelId { get; set; }
    public LevelEntity Level { get; set; }

    [Column("user_id")]
    public int? UserId { get; set; }
    public UserEntity User { get; set; }
}