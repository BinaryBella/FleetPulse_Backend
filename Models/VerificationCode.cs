namespace FleetPulse_BackEndDevelopment.Models;
public class VerificationCode
{
    public int Id { get; set; }
    public string Code { get; set; }
    public string Status { get; set; }
    public DateTime ExpirationDateTime { get; set; }
    public string Email { get; set; }
}