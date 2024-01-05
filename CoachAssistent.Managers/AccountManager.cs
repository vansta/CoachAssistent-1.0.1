using AutoMapper;
using CoachAssistent.Data;
using CoachAssistent.Managers.Email;
using CoachAssistent.Managers.Helpers;
using CoachAssistent.Models.Domain;
using CoachAssistent.Models.ViewModels.Email;
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
    public class AccountManager(CoachAssistentDbContext dbContext, IMapper mapper, IConfiguration configuration, IAuthenticationWrapper authenticationWrapper) : BaseAuthenticatedManager(dbContext, mapper, configuration, authenticationWrapper)
    {
        readonly JwtHelper jwtHelper = new(configuration);
        const int iterations = 32;
        const int saltSize = 20;
        readonly HashAlgorithmName hashAlgorithmName = HashAlgorithmName.SHA256;
        readonly SmtpUtility smtpUtility = new(configuration.GetSection("Smtp").Get<SmtpConfiguration>());
        readonly IConfiguration _configuration = configuration;

        public async Task<string> Register(RegisterViewModel registerData)
        {
            if (dbContext.Users.Any(u => u.UserName.Equals(registerData.UserName)))
            {
                throw new Exception($"Username {registerData.UserName} already exists");
            }

            LoggedInUserViewModel user = await CreateUser(registerData);
            return jwtHelper.GenerateJwt(user);
        }

        public async Task<string> RefreshToken()
        {
            User? user = await dbContext.Users
                .Include(u => u.Memberships)
                .Include(u => u.License)
                .Include(u => u.Tags)
                .FirstOrDefaultAsync(u => u.Id.Equals(authenticationWrapper.UserId));
            return jwtHelper.GenerateJwt(mapper.Map<LoggedInUserViewModel>(user));
        }

        public async Task<string> Login(CredentialsViewModel credentials)
        {
            LoggedInUserViewModel user = await Authenticate(credentials);
            return jwtHelper.GenerateJwt(user);
        }

        public async Task RequestPasswordReset(string userName)
        {
            User? user = await dbContext.Users.FirstOrDefaultAsync(u => u.UserName!.Equals(userName)) ?? throw new Exception("Username not found");
            PasswordResetRequest passwordResetRequest = new()
            {
                UserId = user.Id,
                RequestDateTime = DateTime.Now
            };
            
            await dbContext.PasswordResetRequests.AddAsync(passwordResetRequest);
            await dbContext.SaveChangesAsync();

            string body = File.ReadAllText(Path.Combine(Directory.GetCurrentDirectory(), "Content/Templates/PasswordReset/nl.html"));
            body = body.Replace("{{REQUEST_URL}}", $"{_configuration["ClientUrl"]}/ResetPassword/{passwordResetRequest.Id}");
            body = body.Replace("{{USERNAME}}", user.UserName);
            Content content = new()
            {
                Subject = "Password reset",
                Body = body,
                To = user.Email
            };

            await smtpUtility.SendMailAsync(content);
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
            EncryptPassword(user, registerData.PasswordHash);
            //using (Rfc2898DeriveBytes rfc2898DeriveBytes = new(registerData.PasswordHash, saltSize, iterations, hashAlgorithmName))
            //{
            //    user.Salt = rfc2898DeriveBytes.Salt;
            //    user.Key = rfc2898DeriveBytes.GetBytes(saltSize);
            //}

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
                .Include(u => u.Tags)
                .FirstOrDefaultAsync(u => u.UserName.Equals(credentials.UserName)) ?? throw new Exception($"No user found for {credentials.UserName}");
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

        public async Task<ResetPasswordViewModel> ResetRequest(Guid id)
        {
            PasswordResetRequest? passwordResetRequest = await dbContext.PasswordResetRequests
                .Include(prr => prr.User)
                .FirstOrDefaultAsync(prr => prr.Id.Equals(id));

            if (passwordResetRequest is null)
            {
                throw new Exception("Reset request not found");
            }
            else if (passwordResetRequest.ResetDateTime.HasValue)
            {
                throw new Exception("Reset request has already been activated");
            }
            else if (passwordResetRequest.RequestDateTime < DateTime.Now.AddHours(-3))
            {
                throw new Exception("Reset request has expired");
            }
            else
            {
                return new ResetPasswordViewModel
                {
                    Id = passwordResetRequest.Id,
                    UserName = passwordResetRequest.User!.UserName
                };
            }
        }

        public async Task<string> ResetPassword(ResetPasswordViewModel model)
        {
            PasswordResetRequest? passwordResetRequest = await dbContext.PasswordResetRequests
                .FindAsync(model.Id);

            User? user = await dbContext.Users.FindAsync(passwordResetRequest!.UserId);

            if (passwordResetRequest is null)
            {
                throw new Exception("Reset request not found");
            }
            else if (passwordResetRequest.ResetDateTime.HasValue)
            {
                throw new Exception("Reset request has already been activated");
            }
            else if (passwordResetRequest.RequestDateTime < DateTime.Now.AddHours(-3))
            {
                throw new Exception("Reset request has expired");
            }
            else
            {
                EncryptPassword(user!, model.PasswordHash!);
                passwordResetRequest.ResetDateTime = DateTime.Now;
                await dbContext.SaveChangesAsync();
            }

            return jwtHelper.GenerateJwt(mapper.Map<LoggedInUserViewModel>(user));
        }

        private void EncryptPassword(User user, string passwordHash)
        {
            using Rfc2898DeriveBytes rfc2898DeriveBytes = new(passwordHash, saltSize, iterations, hashAlgorithmName);
            user.Salt = rfc2898DeriveBytes.Salt;
            user.Key = rfc2898DeriveBytes.GetBytes(saltSize);
        }
    }
}
