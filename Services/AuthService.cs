using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FleetPulse_BackEndDevelopment.Data;
using FleetPulse_BackEndDevelopment.Models;
using FleetPulse_BackEndDevelopment.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace FleetPulse_BackEndDevelopment.Services;

public class AuthService : IAuthService
{
    private readonly FleetPulseDbContext dataContext;
        private readonly IConfiguration configuration;

        public AuthService(FleetPulseDbContext dataContext, IConfiguration configuration)
        {
            this.dataContext = dataContext;
            this.configuration = configuration;
        }

        public bool IsAuthenticated(string username, string password)
        {
            var user = this.GetByUsername(username);
            return this.DoesUserExists(username) && BCrypt.Net.BCrypt.Verify(password, user.HashedPassword);
        }

        public bool DoesUserExists(string username)
        {
            var user = dataContext.Users.FirstOrDefault(x => x.UserName == username);
            return user != null;
        }

        public bool DoesEmailExists(string email)
        {
            var user = dataContext.Users.FirstOrDefault(x => x.EmailAddress == email);
            return user != null;
        }
        
        public string GetUsernameByEmail(string email)
        {
            var user = dataContext.Users.FirstOrDefault(x => x.EmailAddress == email);
            return user != null ? user.UserName : null;
        }

        public User GetById(int id)
        {
            return dataContext.Users.FirstOrDefault(c => c.UserId == id);
        }
        
        public User? GetByNic(string nic)
        {
            return dataContext.Users.FirstOrDefault(c => c.NIC == nic);
        }
    
        public User[] GetAll()
        {
            return this.dataContext.Users.ToArray();
        }

        public User? GetByUsername(string username)
        {
            return dataContext.Users.FirstOrDefault(c => c.UserName == username);
        }
        
        public User RegisterUser(User model)
        {
            model.HashedPassword = BCrypt.Net.BCrypt.HashPassword(model.HashedPassword);

            var userEntity = dataContext.Users.Add(model);
            dataContext.SaveChanges();

            return userEntity.Entity;
        }

        public string GenerateJwtToken(string username, string JobTitle)
        {
            var issuer = this.configuration["Jwt:Issuer"];
            var audience = this.configuration["Jwt:Audience"];
            var key = Encoding.ASCII.GetBytes(this.configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                            new Claim("Id", Guid.NewGuid().ToString()),
                            new Claim(JwtRegisteredClaimNames.Sub, username),
                            new Claim(JwtRegisteredClaimNames.Email, username),
                            new Claim(ClaimTypes.Role, JobTitle),
                            new Claim(JwtRegisteredClaimNames.Jti,
                            Guid.NewGuid().ToString())
                        }),
                Expires = DateTime.UtcNow.AddMinutes(5),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public string DecodeEmailFromToken(string token)
        {
            var decodedToken = new JwtSecurityTokenHandler();
            var indexOfTokenValue = 7;

            var t = decodedToken.ReadJwtToken(token.Substring(indexOfTokenValue));

            return t.Payload.FirstOrDefault(x => x.Key == "username").Value.ToString();
        }

        public User ChangeRole(string username, string JobTitle)
        {
            var user = this.GetByUsername(username);
            user.JobTitle = JobTitle;
            this.dataContext.SaveChanges();
            
            return user;
        }
        
        public User GetByEmail(string email)
        {
            return dataContext.Users.FirstOrDefault(c => c.EmailAddress == email);
        }
        
        public bool ResetPassword(string email, string newPassword)
        {
            var user = GetByEmail(email);
            if (user != null)
            {
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(newPassword);

                user.HashedPassword = hashedPassword;
                dataContext.SaveChanges();

                return true; 
            }
            return false; 
        }
        
        public async Task<IEnumerable<User?>> GetAllUsersAsync()
        {
            return await dataContext.Users.ToListAsync();
        }
        
        public async Task<User?> GetUserIdAsync(int UserId)
        {
            return await dataContext.Users.FindAsync(UserId);
        }
        public async Task<User?> AddUserAsync(User? User)
        {
            dataContext.Users.Add(User);
            await dataContext.SaveChangesAsync();
            return User;
        }
        public async Task<bool> UpdateUserAsync(User User)
        {
            dataContext.Entry(User).State = EntityState.Detached;
            var result = dataContext.Users.Update(User);
            result.State = EntityState.Detached;
            await dataContext.SaveChangesAsync();

            if (result.State == EntityState.Modified)
            {
                return true;
            }
            return false;
        }
 
        public async Task<bool> DeactivateUserAsync(User User)
        {
            dataContext.Entry(User).State = EntityState.Detached;
            
            User.Status = false;
            
            var result = dataContext.Users.Update(User);
            
            await dataContext.SaveChangesAsync();
            
            if (result.State == EntityState.Modified)
            {
                return true;
            }
            return false;       
        }
}
