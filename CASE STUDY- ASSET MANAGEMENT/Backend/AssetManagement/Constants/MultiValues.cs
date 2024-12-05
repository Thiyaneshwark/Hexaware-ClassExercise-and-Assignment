using System;
using System.Text.Json.Serialization;

namespace AssetManagement.Models;
public class MultiValues
{
    public enum AssetStatus
    {
        OpenToRequest,
        Allocated,
        UnderMaintenance
    }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum UserType
    {
        Employee,
        Admin
    }

    public enum RequestStatus
    {
        Pending,
        Allocated,
        Rejected,
        Approved,
        Denied,  
    }

    public enum IssueType
    {
        Malfunction,
        Repair,
        Installation,
        Software_Issue  
    }


    public enum AuditStatus
    {
        Sent,
        Completed,
        Returned
    }

    public enum ServiceReqStatus
    {
        UnderReview,
        Approved,
        Completed
    }

    public enum ReturnReqStatus
    {
        Sent,
        Approved,
        Returned,
        Rejected
    }

}
