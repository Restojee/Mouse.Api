using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mouse.NET.Data.Models;

[Table("tips")]
public class TipEntity : AuditableEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [ForeignKey("title")]
    public string Title { get; set; }
    
    [ForeignKey("text")]
    public string Text { get; set; }

    [Column("user_id")]
    [ForeignKey("User")]
    public int UserId { get; set; }
    public UserEntity User { get; set; }
}