using eCommerce.Core.DTO;
using eCommerce.Core.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace eCommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _AuthService;
        public AuthController(IAuthService authService)
        {
            _AuthService = authService;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterRequestDto req)
        {
            if (ModelState.IsValid)
            {
                var result = await _AuthService.Register(req);
                return Ok(result);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        [EnableRateLimiting("per-user")]
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginRequestDto req)
        {
            if (ModelState.IsValid)
            {

                var result = await _AuthService.Login(req);
                if (!result.IsAuthenticated)
                {
                    return Unauthorized(new { Message = result.Message });
                }
                Response.Cookies.Append("refreshToken", result.RefreshToken, new CookieOptions
                {
                    HttpOnly = true,
                    Expires = DateTime.UtcNow.AddDays(7)
                });
                return Ok(new
                {
                    AccessToken = result.AccessToken
                });
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        [HttpPost("RefreshToken")]
        public async Task<IActionResult> RefreshToken()
        {
            var refreshToken = Request.Cookies["refreshToken"];
            if (refreshToken == null)
            {
                return BadRequest(new { Message = "Refresh token is missing." });
            }
            var result = await _AuthService.RefreshToken(refreshToken);
            Response.Cookies.Append("refreshToken", result.RefreshToken, new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7)
            });
            return Ok(new { AccessToken= result.AccessToken });
        }
    }
}
