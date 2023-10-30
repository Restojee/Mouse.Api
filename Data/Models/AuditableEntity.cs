using System.ComponentModel.DataAnnotations.Schema;

public class AuditableEntity
{
    [Column("created_utc_date")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime? CreatedUtcDate { get; set; } = DateTime.UtcNow;

    [Column("modified_utc_date")]
    [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
    public DateTime? ModifiedUtcDate { get; set; } = DateTime.UtcNow;
}