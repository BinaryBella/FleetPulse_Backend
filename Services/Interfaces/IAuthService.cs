using FleetPulse_BackEndDevelopment.Models;

namespace FleetPulse_BackEndDevelopment.Services.Interfaces;

public interface IAuthService
{
    User IsAuthenticated(string username, string password);
    bool DoesUserExists(string username);
    bool DoesEmailExists(string email);
    User GetById(int id);
    User[] GetAll();
    User? GetByUsername(string username);
    User RegisterUser(User model);
    string GenerateJwtToken(string username, string JobTitle);
    string DecodeEmailFromToken(string token);
    User ChangeRole(string username, string JobTitle);
    bool ResetPassword(string email, string newPassword);
    string GetUsernameByEmail(string email);
    public Task<User?> GetUserByUsernameAsync(string username);
    Task<User?> AddUserAsync(User? User);
    Task<bool> UpdateUserAsync(User User);
    Task<bool> ResetPasswordAsync(string email, string newPassword);

}