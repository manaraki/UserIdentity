using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using UserIdentity.Application.Dtos;
using UserIdentity.Application.Interfaces;
using UserIdentity.Domain.Entities.User;

namespace UserIdentity.Application.Services
{
    public class TokenService : ITokenService
    {        
        public string GenerateToken(User user,TokenDto token)
        {
            if (token == null || string.IsNullOrEmpty(token.SecretKey) || string.IsNullOrEmpty(token.Issuer) || string.IsNullOrEmpty(token.Audience))
            {
                throw new ArgumentNullException("Invalid token configuration");
            }

            var securityKey = new SymmetricSecurityKey(
                Encoding.ASCII.GetBytes(token.SecretKey));

            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claimsForToken = new List<Claim>();
            claimsForToken.Add(new Claim("userId", user.Id.ToString()));
            claimsForToken.Add(new Claim("username", user.Username.ToString()));
            var userRoles = new List<string>();
            if (user.UserRoles != null) 
            {
                foreach (var role in user.UserRoles)
                {
                    userRoles.Add(role.Role.RoleTiltle);
                }
            }
                        
            claimsForToken.Add(new Claim("userRole", userRoles.ToString()));



            var jwtSecurityToken = new JwtSecurityToken(
                token.Issuer,
                token.Audience,                
                claimsForToken,
                DateTime.UtcNow,
                DateTime.UtcNow.AddHours(1),
                signingCredentials);

            var Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            return Token;
        }

        public UserFromTokenDto GetUserFromToken(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadJwtToken(token);

            // Extract user information from the token claims
            var userId = int.Parse(jwtToken.Claims.FirstOrDefault(c => c.Type == "userId").Value);
            var username = jwtToken.Claims.FirstOrDefault(c => c.Type == "username").Value;
            var roles = jwtToken.Claims.Where(c => c.Type == "userRole").Select(c=> c.Value).ToList();

            
            return new UserFromTokenDto
            {
                Id = userId,
                UserName = username,
                Roles = roles
            };
        }
    }
}
