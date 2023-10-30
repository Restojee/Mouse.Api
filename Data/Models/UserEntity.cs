using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace Mouse.NET.Data.Models;

[Table("users")]
public class UserEntity : IdentityUser
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    
    [Column("created_utc_date")]
    public DateTime? CreatedUtcDate { get; set; } = DateTime.UtcNow;

    [Column("modified_utc_date")]
    public DateTime? ModifiedUtcDate { get; set; } = DateTime.UtcNow;

    [Column("salt")]
    public byte[] Salt { get; set; }
    
    public ICollection<LevelEntity> Levels { get; set; }
    public ICollection<LevelCommentEntity> Comments { get; set; }
    public ICollection<LevelNoteEntity> Notes { get; set; }
    public ICollection<TipEntity> Tips { get; set; }
    public ICollection<LevelCompletedEntity> Completed { get; set; }
    public ICollection<LevelFavoriteEntity> Favorites { get; set; }
    
}