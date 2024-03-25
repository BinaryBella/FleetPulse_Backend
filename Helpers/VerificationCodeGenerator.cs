namespace FleetPulse_BackEndDevelopment.Helpers;

public class VerificationCodeGenerator
{
    private static readonly Random _random = new ();
    
    public static string GenerateCode()
    {
        return _random.Next(100_000, 1_000_000).ToString();
    }
}