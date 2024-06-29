namespace FleetPulse_BackEndDevelopment.Data.DTO;

public class StaffDTO
{
    public int UserId { get; set; }
    public string UserName { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string NIC { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string PhoneNo { get; set; }
    public string EmailAddress { get; set; }
    public string ProfilePicture { get; set; }
}