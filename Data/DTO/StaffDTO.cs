namespace FleetPulse_BackEndDevelopment.Data.DTO;

public class StaffDTO
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string NIC { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string PhoneNo { get; set; }
    public string EmailAddress { get; set; }
    public byte[]? ProfilePicture { get; set; }
    public bool Status { get; set; }
}