using eCommerce.Core.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace eCommerce.Core.Interfaces
{
    public interface IAuthService
    {
        public Task<AuthenticationResponse> Login(LoginRequestDto request);
        public Task<RegisterResponse> Register(RegisterRequestDto request);
        public Task<AuthenticationResponse> RefreshToken(string refreshToken);
    }
}
