using LMS_ConsumeAPP.Application.DTOs.User;
using LMS_ConsumeAPP.Domain.Model;

namespace LMS_ConsumeAPP.Application.Interface.Services.AuthService
{
    public interface IAuthService
    {
        Task<string> AuthenticateAsync(UserDto user);
        
    }
}
