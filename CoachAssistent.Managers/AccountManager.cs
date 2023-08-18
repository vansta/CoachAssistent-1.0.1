using AutoMapper;
using CoachAssistent.Data;
using CoachAssistent.Managers.Helpers;
using CoachAssistent.Models.Domain;
using CoachAssistent.Models.ViewModels.Member;
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
        readonly JwtHelper jwtHelper;
        const int iterations = 32;
        const int saltSize = 20;
        readonly HashAlgorithmName hashAlgorithmName = HashAlgorithmName.SHA256;
        public AccountManager(CoachAssistentDbContext dbContext, IMapper mapper, IConfiguration configuration) : base(dbContext, mapper)
        {
            //this.configuration = configuration;
            jwtHelper = new JwtHelper(configuration);
        }

        public async Task<string> Register(RegisterViewModel registerData)
        {
            if (dbContext.Users.Any(u => u.UserName.Equals(registerData.UserName)))
            {
                throw new Exception($"Username {registerData.UserName} already exists");
            }

            LoggedInUserViewModel user = await CreateUser(registerData);
            return jwtHelper.GenerateJwt(user);
        }

        public async Task<string> Login(CredentialsViewModel credentials)
        {
            LoggedInUserViewModel user = await Authenticate(credentials);
            return jwtHelper.GenerateJwt(user);
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
                LastUpdate = DateTime.Now,
                LicenseId = dbContext.Licenses
                    .OrderBy(l => l.Level)
                    .First().Id
            };

            using (Rfc2898DeriveBytes rfc2898DeriveBytes = new(registerData.PasswordHash, saltSize, iterations, hashAlgorithmName))
            {
                user.Salt = rfc2898DeriveBytes.Salt;
                user.Key = rfc2898DeriveBytes.GetBytes(saltSize);
            }

            user = (await dbContext.Users.AddAsync(user)).Entity;

            if (registerData.GroupIds is not null)
            {
                foreach (var group in registerData.GroupIds.Where(g => !user.Memberships.Select(m => m.GroupId).Contains(g)))
                {
                    await RequestGroupAccess(new MembershipRequestViewModel
                    {
                        UserId = user.Id,
                        GroupId = group
                    });
                }
            }
            
            await dbContext.SaveChangesAsync();

            return mapper.Map<LoggedInUserViewModel>(user);
        }

        private async Task<LoggedInUserViewModel> Authenticate(CredentialsViewModel credentials)
        {
            User? user = await dbContext.Users
                .Include(u => u.Memberships)
                .Include(u => u.License)
                .FirstOrDefaultAsync(u => u.UserName.Equals(credentials.UserName));
            if (user == null)
            {
                throw new Exception($"No user found for {credentials.UserName}");
            }
            using var deriveBytes = new Rfc2898DeriveBytes(credentials.PasswordHash, user.Salt, iterations, hashAlgorithmName);
            byte[] key = deriveBytes.GetBytes(saltSize);
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
