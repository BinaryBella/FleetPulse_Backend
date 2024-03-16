using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FleetPulse_BackEndDevelopment.Data;
using FleetPulse_BackEndDevelopment.Helpers;
using FleetPulse_BackEndDevelopment.Models;
using Microsoft.IdentityModel.Tokens;
using BC = BCrypt.Net.BCrypt;

namespace FleetPulse_BackEndDevelopment.Services;

public class AuthService
{
    private readonly FleetPulseDbContext dataContext;
        private readonly IConfiguration configuration;

        public AuthService(FleetPulseDbContext dataContext, IConfiguration configuration)
        {
            this.dataContext = dataContext;
            this.configuration = configuration;
        }

        public bool IsAuthenticated(string email, string password)
        {
            var user = this.GetByEmail(email);
            return this.DoesUserExists(email) && BC.Verify(password, user.HashedPassword);
        }

        public bool DoesUserExists(string email)
        {
            var user = this.dataContext.Users.FirstOrDefault(x => x.EmailAddress == email);
            return user != null;
        }

        public User GetById(int id)
        {
            return this.dataContext.Users.FirstOrDefault(c => c.UserId == id);
        }

        public User[] GetAll()
        {
            return this.dataContext.Users.ToArray();
        }

        public User GetByEmail(string email)
        {
            return this.dataContext.Users.FirstOrDefault(c => c.EmailAddress == email);
        }

        public User RegisterUser(User model)
        {
            model.HashedPassword = BC.HashPassword(model.HashedPassword);

            var userEntity = dataContext.Users.Add(model);
            dataContext.SaveChanges();

            return userEntity.Entity;
        }

        public string GenerateJwtToken(string email, string? JobTitle)
        {
            var issuer = this.configuration["Jwt:Issuer"];
            var audience = this.configuration["Jwt:Audience"];
            var key = Encoding.ASCII.GetBytes(this.configuration["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                            new Claim("Id", Guid.NewGuid().ToString()),
                            new Claim(JwtRegisteredClaimNames.Sub, email),
                            new Claim(JwtRegisteredClaimNames.Email, email),
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

            return t.Payload.FirstOrDefault(x => x.Key == "email").Value.ToString();
        }

        public User ChangeRole(string email, string JobTitle)
        {
            var user = this.GetByEmail(email);
            user.JobTitle = JobTitle;
            this.dataContext.SaveChanges();


            return user;
        }
}