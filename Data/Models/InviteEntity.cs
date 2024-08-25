using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mouse.NET.Data.Models;

public class InviteEntity : AuditableEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Column("user_id")]
    [ForeignKey("User")]
    public int UserId { get; set; }
    public UserEntity User { get; set; }

    [Column("token")]
    public string Token { get; set; }
    
    [Column("email")]
    public string Email { get; set; }
    
    [Column("is_used")]
    public bool IsUsed { get; set; }
    
    [Column("expiration_date ")]
    public DateTime ExpirationDate  { get; set; }
}