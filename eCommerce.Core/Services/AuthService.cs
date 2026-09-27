using eCommerce.Core.DTO;
using eCommerce.Core.Interfaces;
using eCommerce.Domian.Entities;
using eCommerce.Domian.Interface;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;


namespace eCommerce.Core.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        public AuthService(IUserRepository userRepository, IConfiguration configuration)
        {
            _userRepository = userRepository;
            _configuration = configuration;
        }
        public async Task<AuthenticationResponse> Login(LoginRequestDto request)
        {

            try
            {
                IPasswordHasher<ApplicationUser> hasher = new PasswordHasher<ApplicationUser>();
                var user = await _userRepository.GetAllUser().FirstOrDefaultAsync(u => u.Email == request.Email && u.IsDeleted == false);
                if (user != null)
                {
                    var result = hasher.VerifyHashedPassword(user, user.Password, request.Password);
                    if (result == PasswordVerificationResult.Success)
                    {
                        UsersDto userDto = new UsersDto
                        {
                            Id = user.Id,
                            Name = user.Name,
                            Email = user.Email,
                            Gender = user.Gender,
                            role = user.Roles
                        };
                        var refreshToken = GenerateToken(userDto, 7);
                        user.RefreshToken = refreshToken;
                        _userRepository.UpdateUser(user);
                        return new AuthenticationResponse
                        {
                            AccessToken = GenerateToken(userDto, 15),
                            IsAuthenticated = true,
                            RefreshToken = refreshToken,
                            Message = "Login successful"
                        };
                    }
                    else
                    {
                        return new AuthenticationResponse
                        {
                            IsAuthenticated = false,
                            Message = "Invalid email or password"
                        };
                    }
                }
                else
                {
                    return new AuthenticationResponse
                    {
                        IsAuthenticated = false,
                        Message = "Invalid email or password"
                    };
                }
            }
            catch (Exception ex)
            {
                return new AuthenticationResponse
                {
                    IsAuthenticated = false,
                    Message = $"Login failed: {ex.Message}"
                };
            }
        }

        public async Task<RegisterResponse> Register(RegisterRequestDto request)
        {
            try
            {
                IPasswordHasher<ApplicationUser> hasher = new PasswordHasher<ApplicationUser>();


                ApplicationUser user = new ApplicationUser()
                {
                    Name = request.Name,
                    Email = request.Email,
                    Gender = request.Gender,
                    Roles= request.Roles
                };
                var hashedPassword = hasher.HashPassword(user, request.Password);
                user.Password = hashedPassword;
                await _userRepository.AddUserAsync(user);
                return new RegisterResponse
                {
                    Message = "User registered successfully",
                    Code = System.Net.HttpStatusCode.OK
                };
            }
            catch (Exception ex)
            {
                return new RegisterResponse
                {
                    Message = $"User registration failed: {ex.Message}",
                    Code = System.Net.HttpStatusCode.InternalServerError
                };
            }

        }
        private string GenerateToken(UsersDto user, int expiry)
        {
            if (expiry == 7)
            {
                var claims = new[]
            {
                new Claim("UserId",user.Id.ToString()),
                new Claim("UserName",user.Name),
                new Claim("Email",user.Email),
                new Claim("role", user.role?.ToString() ?? "User")
            };
                //Bring The Key
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                //Encrypt it
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                //make the token
                var token = new JwtSecurityToken(issuer: _configuration["Jwt:Issuer"], audience: _configuration["Jwt:Audience"], claims: claims, expires: DateTime.UtcNow.AddDays(expiry), signingCredentials: creds);
                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            else
            {
                var claims = new[]
                {
                new Claim("UserId",user.Id.ToString()),
                new Claim("UserName",user.Name),
                new Claim("Email",user.Email),
                new Claim("IsAdmin","false"),
                new Claim("role", user.role?.ToString() ?? "User")
                 };
                //Bring The Key
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
                //Encrypt it
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                //make the token
                var token = new JwtSecurityToken(issuer: _configuration["Jwt:Issuer"], audience: _configuration["Jwt:Audience"], claims: claims, expires: DateTime.UtcNow.AddMinutes(expiry), signingCredentials: creds);
                return new JwtSecurityTokenHandler().WriteToken(token);
            }
        }

        public async Task<AuthenticationResponse> RefreshToken(string refreshToken)
        {
            try
            {
                var user = await _userRepository.GetAllUser().FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
                if (user != null)
                {
                    UsersDto userDto = new UsersDto
                    {
                        Id = user.Id,
                        Name = user.Name,
                        Email = user.Email,
                        Gender = user.Gender,
                        role = user.Roles
                    };
                    var refreshTokenNew = GenerateToken(userDto, 7);
                    user.RefreshToken = refreshTokenNew;
                    _userRepository.UpdateUser(user);
                    return new AuthenticationResponse
                    {
                        Id = user.Id,
                        RefreshToken = refreshTokenNew,
                        AccessToken = GenerateToken(userDto, 15),
                        Message = "Token refreshed successfully",

                    };
                }
                else
                {
                    return new AuthenticationResponse
                    {
                        IsAuthenticated = false,
                        Message = "Invalid refresh token"
                    };
                }
            }
            catch (Exception ex)
            {
                return new AuthenticationResponse
                {
                    IsAuthenticated = false,
                    Message = $"Token refresh failed: {ex.Message}"
                };
            }
        }
    }
}
