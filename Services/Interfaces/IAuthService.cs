using NewWebApplication.DTOs;

namespace NewWebApplication.Services.Interfaces
{
    public interface IAuthService
    {
        bool Register(RegisterDto dto);

        LoginResponseDto Login(LoginDto dto);
    }
}