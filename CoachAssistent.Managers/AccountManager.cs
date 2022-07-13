using AutoMapper;
using CoachAssistent.Data;
using CoachAssistent.Models.Domain;
using CoachAssistent.Models.ViewModels.User;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CoachAssistent.Managers
{
    public class AccountManager : BaseManager
    {
        readonly IConfiguration configuration;
        public AccountManager(CoachAssistentDbContext dbContext, IMapper mapper, IConfiguration configuration) : base(dbContext, mapper)
        {
            this.configuration = configuration;
        }

        public async Task<string> Register(RegisterViewModel registerData)
        {
            if (dbContext.Users.Any(u => u.UserName.Equals(registerData.UserName)))
            {
                throw new Exception($"Username {registerData.UserName} already exists");
            }

            LoggedInUserViewModel user = await CreateUser(registerData);
            return GenerateJwt(user);
        }

        public async Task<string> Login(CredentialsViewModel credentials)
        {
            LoggedInUserViewModel user = await Authenticate(credentials);
            return GenerateJwt(user);
        }

        private async Task<LoggedInUserViewModel> CreateUser(RegisterViewModel registerData)
        {
            User user = new()
            {
                UserName = registerData.UserName,
                Email = registerData.Email,
                FirstName = registerData.FirstName,
                LastName = registerData.LastName,
                CreationDate = DateTime.Now,
                LastUpdate = DateTime.Now
            };

            using (Rfc2898DeriveBytes rfc2898DeriveBytes = new(registerData.PasswordHash, 20))
            {
                user.Salt = rfc2898DeriveBytes.Salt;
                user.Key = rfc2898DeriveBytes.GetBytes(20);
            }

            user = (await dbContext.Users.AddAsync(user)).Entity;
            await dbContext.SaveChangesAsync();

            return mapper.Map<LoggedInUserViewModel>(user);
        }

        private string GenerateJwt(LoggedInUserViewModel user)
        {
            IConfigurationSection jwtSection = configuration.GetSection("Jwt");
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSection["Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email ?? "")
            };

            JwtSecurityToken jwt = new JwtSecurityToken(
                issuer: jwtSection["Issuer"],
                audience: jwtSection["Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(8),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(jwt);
        }

        private async Task<LoggedInUserViewModel> Authenticate(CredentialsViewModel credentials)
        {
            User? user = await dbContext.Users.FirstOrDefaultAsync(u => u.UserName.Equals(credentials.UserName));
            if (user == null)
            {
                throw new Exception($"No user found for {credentials.UserName}");
            }
            using var deriveBytes = new Rfc2898DeriveBytes(credentials.PasswordHash, user.Salt);
            byte[] key = deriveBytes.GetBytes(20);
            if (key.SequenceEqual(user.Key))
            {
                return mapper.Map<LoggedInUserViewModel>(user);
            }
            else
            {
                throw new Exception("Incorrect password");
            }
            
        }
    }
}
