using NewWebApplication.DTOs;

namespace NewWebApplication.Services.Interfaces
{
    public interface IAuthService
    {
        bool Register(RegisterDto dto);

        string Login(LoginDto dto);
    }
}