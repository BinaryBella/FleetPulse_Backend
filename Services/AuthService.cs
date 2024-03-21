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

        public bool IsAuthenticated(string username, string password)
        {
            var user = this.GetByUsername(username);
            return this.DoesUserExists(username) && BC.Verify(password, user.HashedPassword);
        }

        public bool DoesUserExists(string username)
        {
            var user = this.dataContext.Users.FirstOrDefault(x => x.UserName == username);
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

        public User GetByUsername(string username)
        {
            return this.dataContext.Users.FirstOrDefault(c => c.UserName == username);
        }

        public User RegisterUser(User model)
        {
            model.HashedPassword = BC.HashPassword(model.HashedPassword);

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
}