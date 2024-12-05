﻿using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using static AssetManagement.Models.MultiValues;
public class ServiceRequest
{
    //[Required]
    [Key]
    public int ServiceId { get; set; }

    //[Required]
    public int AssetId { get; set; }

    //[Required]
    public int UserId { get; set; }

    //[Required]
    [DataType(DataType.Date)]
    [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
    public DateTime ServiceRequestDate { get; set; }

    //[Required]
    public IssueType Issue_Type { get; set; }

    //[Required]
    public string? ServiceDescription { get; set; }

    //[Required]
    [DefaultValue(ServiceReqStatus.UnderReview)]
    public ServiceReqStatus ServiceReqStatus { get; set; } = ServiceReqStatus.UnderReview;

    //Navigation Properties
    // 1 - 1 Relation

    public Asset? Asset { get; set; }
    [JsonIgnore]
    public User? User { get; set; }
}
