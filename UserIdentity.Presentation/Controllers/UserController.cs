using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserIdentity.Application.Dtos;
using UserIdentity.Application.Interfaces;
using UserIdentity.Application.Services;
using UserIdentity.Domain.Entities.User;
using UserIdentity.Presentation.ApiResponse;

namespace UserIdentity.Presentation.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IRegisterUserService _registerUserService;
        private readonly IAuthenticateUserService _authenticateUserService;
        private readonly IUserService _userService;


        private readonly ITokenService _tokenService;
        IConfiguration _configuration;

        public UserController(IRegisterUserService registerUserService, 
            IAuthenticateUserService authenticateUserService,IConfiguration configuration,
            ITokenService tokenService,IUserService userService)
        {
            _registerUserService = registerUserService;
            _authenticateUserService = authenticateUserService;
            _configuration = configuration;
            _tokenService = tokenService;
            _userService = userService;

        }
        [HttpPost("register")]
        public IActionResult Register(UserRegisterDto userRegisterDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Data");
            }

            var userId=_registerUserService.Execute(userRegisterDto).Result;
            var response = new ApiResponse<RegisterApiResponse>
            {
                Message = "Register successful",
                ResponseStatus = 200,
                Data = new RegisterApiResponse
                {
                    UserId = userId,
                    UserName = userRegisterDto.Username
                }
            };
            return Ok(response);

        }
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserAuthenticateDto userAuthenticateDto)
        { 
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Data");
            }
            try
            {
                var user = await _authenticateUserService.Execute(userAuthenticateDto);
                TokenDto token = new TokenDto()
                {
                    SecretKey = _configuration["Authentication:SecretForKey"],
                    Issuer = _configuration["Authentication:Issuer"],
                    Audience = _configuration["Authentication:Audience"]
                };

                string Token = _tokenService.GenerateToken(user, token);

                // Create a new cookie
                var cookieOptions = new CookieOptions
                {

                    Expires = DateTime.Now.AddDays(7), // Set the expiration date of the cookie
                    HttpOnly = true, // Make the cookie accessible only through HTTP requests
                    Secure = true, // Require HTTPS to send the cookie
                    SameSite = SameSiteMode.Strict // Restrict the cookie to the same site
                };


                // Set the token as a cookie
                Response.Cookies.Append("userId", user.Id.ToString(), cookieOptions);
                Response.Cookies.Append("token", Token, cookieOptions);

                var response = new ApiResponse<LoginApiResponse>
                {
                    Message = "Login successful",
                    ResponseStatus = 200,
                    Data = new LoginApiResponse
                    {
                        UserId = user.Id,
                        Token = Token
                    }
                };
                return Ok(response);
            }
            catch 
            {
                var Response = new ApiResponse<LoginApiResponse>
                {
                    ResponseStatus = 404,
                    Message = "user not found"
                };
                return NotFound(Response);
            }            
            
            
           
        }
        
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {     
            
            // Sign out the user
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // Delete the cookie from the response
            Response.Cookies.Delete("token");
            Response.Cookies.Delete("userId");
            return Ok("Logout successful");
        }       
        
    }
}
