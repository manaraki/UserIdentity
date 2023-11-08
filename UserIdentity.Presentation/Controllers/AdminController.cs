using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using UserIdentity.Application.Dtos;
using UserIdentity.Application.Interfaces;
using UserIdentity.Application.Services;
using UserIdentity.Domain.Entities.User;
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

        [HttpGet]
        
        public IActionResult AdminPanel()
        {            
            return Ok("Admin Panel");
        }

        [HttpPost("user/add")]        
       
        public async Task<IActionResult> AddUser(AddUserDto addUserDto)
        {
            var userId = await _addUserService.Execute(addUserDto);
            return Ok(userId);
        }

        

        

    }
}
