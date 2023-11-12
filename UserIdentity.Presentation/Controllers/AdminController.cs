using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using UserIdentity.Application.Dtos;
using UserIdentity.Application.Interfaces;
using UserIdentity.Application.Services;
using UserIdentity.Domain.Entities.User;
using UserIdentity.Presentation.ApiResponse;
using UserIdentity.Presentation.Attributes;

namespace UserIdentity.Presentation.Controllers
{
    [ApiController]
    [Route("api/admin")]
    [CustomAuthorization("Admin")]    
    public class AdminController : ControllerBase
    {
        private readonly IAddUserService _addUserService;        
        private readonly IUserService _userService;

        public AdminController(IAddUserService addUserService, IUserService userService)
        {
            _addUserService = addUserService;            
            _userService = userService;
        }

               
        [HttpPost("user/add")]       

        public async Task<IActionResult> AddUser(AddUserDto addUserDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid Data");
            }
            try
            {
                var userId = await _addUserService.Execute(addUserDto);
                var Response = new ApiResponse<AddUserApiResponse>
                {
                    ResponseStatus = 201,
                    Message = "user added",
                    Data = new AddUserApiResponse()
                    {
                        UserID=userId,
                    }
                };
                return Ok(Response);

            }
            catch
            {
                var Response = new ApiResponse<LoginApiResponse>
                {
                    ResponseStatus = 400,
                    Message = "invalid data"
                };
                return BadRequest(Response);
            }
        }

        

        

    }
}
