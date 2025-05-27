using Application.Interfaces;
using Domain.Entities;
using Domain.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Services
{
    public class TokenService : ITokenService
    {
        private readonly JWTSettings _jwtSettings;
        private readonly SymmetricSecurityKey _key;

        public TokenService(IOptions<JWTSettings> jwtSettings)
        {
            _jwtSettings = jwtSettings.Value ?? throw new ArgumentNullException(nameof(jwtSettings));
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
        }

        public Task<string> CreateToken(Users user)
        {
            var claims = new List<Claim>
            {
                new Claim("Email", user.Email ?? ""),
                new Claim("Id", user.Id.ToString()),
                new Claim("FullName", $"{user.FirstName} {user.LastName}".Trim()),
                new Claim("Phone", user.Phone ?? ""),
                new Claim("isActive", "true"),
                new Claim("isFirstLogin", user.IsFirstLogin?.ToString() ?? "false"),
                new Claim("createdAt", user.CreatedAt.ToString()),
                new Claim("updatedAt", user.UpdatedAt.ToString()),
                new Claim("token", "access_token")
            };

            // Roles
            if (user.UserRoles != null)
            {
                foreach (var userRole in user.UserRoles)
                {
                    if (userRole.Role != null)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, userRole.Role.RoleName));
                    }
                }
            }

            var credentials = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                SigningCredentials = credentials,
                Issuer = _jwtSettings.Issuer,
                Audience = _jwtSettings.Audience
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Task.FromResult(tokenHandler.WriteToken(token));
        }
    }
}
