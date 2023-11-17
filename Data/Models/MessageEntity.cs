using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mouse.NET.Data.Models;

[Table("messages")]
public class MessageEntity : AuditableEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column("text")]
    public string Text { get; set; }
    
    [Column("user_id")]
    [ForeignKey("User")]
    public int UserId { get; set; }
    public UserEntity User { get; set; }
    
    [Column("created_utc_date")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime? CreatedUtcDate { get; set; } = DateTime.UtcNow;

    [Column("modified_utc_date")]
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime? ModifiedUtcDate { get; set; } = DateTime.UtcNow;
}