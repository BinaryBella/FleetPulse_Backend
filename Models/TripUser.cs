namespace FleetPulse_BackEndDevelopment.Models;

public class TripUser
{
    internal int userid;

    public int UserId { get; set; }
    public User User { get; set; }

    public string TripId { get; set; }
    public Trip Trip { get; set; }
}