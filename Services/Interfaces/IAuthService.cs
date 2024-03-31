using FleetPulse_BackEndDevelopment.Models;

namespace FleetPulse_BackEndDevelopment.Services.Interfaces;

public interface IAuthService
{
    bool IsAuthenticated(string username, string password);
    bool DoesUserExists(string username);
    bool DoesEmailExists(string email);
    User GetById(int id);
    User[] GetAll();
    User GetByUsername(string username);
    User RegisterUser(User model);
    string GenerateJwtToken(string username, string JobTitle);
    string DecodeEmailFromToken(string token);
    User ChangeRole(string username, string JobTitle);
}