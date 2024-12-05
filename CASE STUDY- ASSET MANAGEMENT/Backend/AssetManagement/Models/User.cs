﻿using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using System.Text.Json.Serialization;
using static AssetManagement.Models.MultiValues;
using static AssetManagement.Models.MultiValues;


public class User
{
    //[Required]
    [Key]
    public int UserId { get; set; }

    //[Required]
    [MaxLength(55)]
    public string? UserName { get; set; }

    //[Required]
    [EmailAddress]
    public string? UserMail { get; set; }

    //[Required]
    public string? Gender { get; set; }

    //[Required]
    public string? Dept { get; set; }

    //[Required]
    public string? Designation { get; set; }

    //[Required]
    [Phone(ErrorMessage = "Please enter a valid phone number")]
    public string? PhoneNumber { get; set; }

    //[Required]
    public string? Address { get; set; }

    //[Required]
    public string? Branch { get; set; }

    //[Required]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters long")]
    [RegularExpression(@"^(?=.*[A-Za-z])(?=.*\d)(?=.*[A-Z])(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
        ErrorMessage = "Password must contain Uppercase, alphanumeric and special characters")]

    public string? Password { get; set; }

    //[Required]
   // [DefaultValue(UserType.Employee)]
    public UserType User_Type { get; set; } 
        //= UserType.Employee;

    //Navigation Properties
    // 1 - 1 Relation
    [JsonIgnore]
    public UserProfile? UserProfile { get; set; }

    // 1 - * Relation
    [JsonIgnore]
    public ICollection<AssetRequest>? AssetRequests { get; set; } = new List<AssetRequest>();
    [JsonIgnore]
    public ICollection<ServiceRequest>? ServiceRequests { get; set; } = new List<ServiceRequest>();
    [JsonIgnore]
    public ICollection<AssetAllocation>? AssetAllocations { get; set; } = new List<AssetAllocation>();
    [JsonIgnore]
    public ICollection<ReturnRequest>? ReturnRequests { get; set; } = new List<ReturnRequest>();
    [JsonIgnore]
    public ICollection<Audit>? Audits { get; set; } = new List<Audit>();
    [JsonIgnore]
    public ICollection<MaintenanceLog>? MaintenanceLogs { get; set; } = new List<MaintenanceLog>();


}
