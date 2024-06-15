﻿using FleetPulse_BackEndDevelopment.Models;
using System.ComponentModel.DataAnnotations;

namespace FleetPulse_BackEndDevelopment.DTOs;

public class StaffDTO
{
    public int UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string NIC { get; set; }
    public string EmailAddress { get; set; }
    public string PhoneNo { get; set; }
    public string EmergencyContact { get; set; }
    public string? JobTitle { get; set; } // Specific to Staff
    public bool Status { get; set; }
}

