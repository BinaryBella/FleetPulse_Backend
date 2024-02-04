using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FleetPulse_BackEndDevelopment.Data
{
    public class Helper
    {
        public int HelperId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NIC { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNo { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string EmailAddress { get; set; }
        public string EmergencyContact { get; set; }
        public byte[] ProfilePicture { get; set; }
        public bool Status { get; set; }

    }
}