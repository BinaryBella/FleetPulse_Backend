namespace FleetPulse_BackEndDevelopment.Data
{
    public class Staff
    {
        public int StaffId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NIC { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNo { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string EmailAddress { get; set; }
        public string JobTitle { get; set; }
        public byte[] ProfilePicture { get; set; }
        public bool Status { get; set; }
    }
}