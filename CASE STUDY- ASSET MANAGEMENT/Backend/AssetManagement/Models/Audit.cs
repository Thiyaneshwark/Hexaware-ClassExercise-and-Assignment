using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static AssetManagement.Models.MultiValues;

public class Audit
{
    //[Required]
    [Key]
    public int AuditId { get; set; }

    //[Required]
    public int AssetId { get; set; }

    //[Required]
    public int UserId { get; set; }

    //[Required]
    [DataType(DataType.Date)]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
    public DateTime AuditDate { get; set; }

    //[Required]
    public string? AuditMessage { get; set; }

    //[Required]
    [DefaultValue(AuditStatus.Sent)]
    public AuditStatus Audit_Status { get; set; } = AuditStatus.Sent;

    //Navigation Properties
    // 1 - 1 Relation

    public virtual Asset? Asset { get; set; }

    public virtual User? User { get; set; }
}
