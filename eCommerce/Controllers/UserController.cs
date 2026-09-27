using eCommerce.Core.DTO;
using eCommerce.Core.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize(Roles ="SuperAdmin,Admin")]
        [HttpGet("GetAllUsers")]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        [Authorize(Roles ="Admin,SuperAdmin")]
        [HttpDelete("DeleteUser/{userId}")]
        public async Task<IActionResult> DeleteUser([FromRoute] Guid userId)
        {
            var response = await _userService.DeleteUser(userId);
            return Ok(response);
        }
        [HttpPut("UpdateUser/{userId}")]
        public async Task<IActionResult> UpdateUser(Guid userId, [FromBody] UsersDto user)
        {
            if (ModelState.IsValid)
            {
                var response = await _userService.UpdateUserAsync(userId, user);
                return Ok(response);
            }
            else
            {
                return BadRequest(ModelState);
            }

        }
    }
}
