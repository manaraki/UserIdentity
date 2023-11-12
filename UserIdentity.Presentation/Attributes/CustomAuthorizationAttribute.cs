using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserIdentity.Application.Interfaces;
using UserIdentity.Domain.Entities.User;

namespace UserIdentity.Presentation.Attributes
{
    public class CustomAuthorizationAttribute : TypeFilterAttribute    
    {        
        public CustomAuthorizationAttribute(string role) : base(typeof(CustomAuthorizationFilter))
        {
            Arguments = new object[] { role };
        }
    }
    public class CustomAuthorizationFilter : IAuthorizationFilter
    {
        private readonly string _role;
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;

        public CustomAuthorizationFilter(string role, IUserService userService, IConfiguration configuration)
        {
            _role = role;
            _userService = userService;
            _configuration = configuration;
        }

        public async void OnAuthorization(AuthorizationFilterContext context)
        {
            
            string token = context.HttpContext.Request.Cookies["token"];
            string userId = context.HttpContext.Request.Cookies["userId"];
            if (token == null)
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_configuration["Authentication:SecretForKey"]);

                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                }, out var validatedToken);
                //var jwtToken = (JwtSecurityToken)validatedToken;
                //var userID = jwtToken.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;

            }
            catch
            {
                context.Result = new UnauthorizedResult();
                return;
            }
            
            // Check if the user has the required role
            var userRoles = _userService.GetUsersRoles(int.Parse(userId));
            var rolesId = new List<int>();
            foreach (var item in userRoles)
            {
                rolesId.Add(item.RoleId);                
            }
            var roles=new List<string>();
            foreach (var item in rolesId)
            {
                roles.Add(_userService.GetRoleAsync(item).Result.RoleTiltle);
            }
            if (!roles.Contains(_role))
            {
                context.Result = new ObjectResult("Forbidden") { StatusCode = 403};
                return;
            }
            
        }
    }

}

