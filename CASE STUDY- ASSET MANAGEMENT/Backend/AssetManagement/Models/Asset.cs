using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Text.Json.Serialization;
using static AssetManagement.Models.MultiValues;

public class Asset
{
    //[Required]
    [Key]
    public int AssetId { get; set; }

    //[Required]
    [MaxLength(55)]
    public string? AssetName { get; set; }

    public string? AssetDescription { get; set; }

    //[Required]
    public int CategoryId { get; set; }

    //[Required]
    public int SubCategoryId { get; set; }

    //public byte[]? AssetImage { get; set; }

    //[Required]
    public string? SerialNumber { get; set; }

    //[Required]
    public string? Model { get; set; }

    //[Required]
    [DataType(DataType.Date)]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
    public DateTime ManufacturingDate { get; set; }

    //[Required]
    [MaxLength(55)]
    public string? Location { get; set; }

    //[Required]
    public decimal Value { get; set; }

    [DataType(DataType.Date)]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
    public DateTime? Expiry_Date { get; set; }

    //[Required]
    [DefaultValue(AssetStatus.OpenToRequest)]
    public AssetStatus AssetStatus { get; set; } = AssetStatus.OpenToRequest;

    // Navigation Properties
    public Category? Category { get; set; }

    public SubCategory? SubCategories { get; set; }

    [JsonIgnore]
    public ICollection<AssetRequest>? AssetRequests { get; set; } = new List<AssetRequest>();

    [JsonIgnore]
    public ICollection<ServiceRequest>? ServiceRequests { get; set; } = new List<ServiceRequest>();

    [JsonIgnore]
    public ICollection<MaintenanceLog>? MaintenanceLogs { get; set; } = new List<MaintenanceLog>();

    [JsonIgnore]
    public ICollection<Audit>? Audits { get; set; } = new List<Audit>();

    [JsonIgnore]
    public ICollection<ReturnRequest>? ReturnRequests { get; set; } = new List<ReturnRequest>();

    [JsonIgnore]
    public ICollection<AssetAllocation>? AssetAllocations { get; set; } = new List<AssetAllocation>();
}
