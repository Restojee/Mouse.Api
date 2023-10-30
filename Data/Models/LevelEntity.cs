namespace Mouse.NET.Data.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

[Table("levels")]
public class LevelEntity : AuditableEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column("name")]
    public string Name { get; set; }
    
    [Column("description")]
    public string Description { get; set; }
    
    [Column("image")]
    public string Image { get; set; }

    [Column("user_id")]
    [ForeignKey("User")]
    public int UserId { get; set; }
    public UserEntity User { get; set; }

    public ICollection<LevelNoteEntity> Notes { get; set; }
    public ICollection<LevelCommentEntity> Comments { get; set; }
    public ICollection<LevelCompletedEntity> Completed { get; set; }
    public ICollection<LevelFavoriteEntity> Favorites { get; set; }
    public ICollection<TagEntity> Tags { get; set; }
    public ICollection<LevelVisitEntity> Visits { get; set; }
    
    [NotMapped]
    public int CompletedCount { get; set; }
    
    [NotMapped]
    
    public int FavoritesCount { get; set; }
    
    [NotMapped]
    public int CommentsCount { get; set; }
    
    [NotMapped]
    public int VisitsCount { get; set; }
    
    [NotMapped]
    public bool IsCompletedByUser { get; set; }
    
    [NotMapped]
    public bool IsFavoriteByUser { get; set; }
}