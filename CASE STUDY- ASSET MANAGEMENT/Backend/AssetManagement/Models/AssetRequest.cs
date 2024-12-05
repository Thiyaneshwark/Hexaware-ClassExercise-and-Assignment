using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static AssetManagement.Models.MultiValues;
using System.ComponentModel;
using System.Text.Json.Serialization;

public class AssetRequest
{
    [Required]
    [Key]
    public int AssetReqId { get; set; }

    //[Required]
    public int UserId { get; set; }

    //[Required]
    public int AssetId { get; set; }

    //[Required]
    public string? CategoryId { get; set; }

    //[Required] // Ensure AssetRequestDetail field is defined
    public string? assetRequest { get; set; } // Renamed to avoid conflict

    //[Required]
    [DataType(DataType.Date)]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
    public DateTime? AssetReqDate { get; set; }

    //[Required]
    public string? AssetReqReason { get; set; }

    //[Required]
    //[DefaultValue(RequestStatus.Pending)]
    public RequestStatus RequestStatus { get; set; } // Ensure enum is handled correctly

    // Navigation Properties
    public Asset? Asset { get; set; }

    [JsonIgnore]
    public User? User { get; set; }

    public AssetAllocation? AssetAllocation { get; set; }
}

