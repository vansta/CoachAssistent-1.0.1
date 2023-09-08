using CoachAssistent.Models.ViewModels.User;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CoachAssistent.Managers.Helpers
{
    public class JwtHelper
    {
        readonly static JwtSecurityTokenHandler _jwtSecurityTokenHandler = new();
        readonly SymmetricSecurityKey securityKey;
        readonly TokenValidationParameters tokenValidationParameters;

        readonly string issuer;
        readonly string audience;

        public TokenValidationParameters TokenValidationParameters
        {
            get
            {
                return tokenValidationParameters;
            }
        }

        public JwtHelper(IConfiguration configuration)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            string secretKey = configuration["Jwt:Key"] ?? string.Empty;
            issuer = configuration["Jwt:Issuer"] ?? string.Empty;
            audience = configuration["Jwt:Audience"] ?? string.Empty;
            securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));

            tokenValidationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = securityKey,
                ValidIssuer = issuer,
                ValidAudience = audience,
                //ValidateLifetime = true,
                ValidateAudience = true,
                ValidateIssuer = true,
                //RequireExpirationTime = true
            };
        }

        public string GenerateJwt(LoggedInUserViewModel user)
        {
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, $"{user.Id}"),
                new Claim(CustomClaimTypes.Id, $"{user.Id}"),
                new Claim(ClaimTypes.Name, user.UserName ?? ""),
                new Claim(CustomClaimTypes.Name, user.UserName ?? ""),
                new Claim(ClaimTypes.Email, user.Email ?? ""),
                new Claim(CustomClaimTypes.Email, user.Email ?? ""),
                new Claim(CustomClaimTypes.License, user.LicenseId.ToString() ?? ""),
                new Claim(CustomClaimTypes.LicenseLevel, user.LicenseLevel ?? "")
            };

            claims.AddRange(user.GroupIds.Select(gi => new Claim(CustomClaimTypes.Groups, gi.ToString())));
            claims.AddRange(user.Tags.Select(t => new Claim(CustomClaimTypes.Tags, t)));

            JwtSecurityToken jwt = new(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: DateTime.Now.AddHours(8),
                signingCredentials: credentials);

            return _jwtSecurityTokenHandler.WriteToken(jwt);
        }
    }
}
