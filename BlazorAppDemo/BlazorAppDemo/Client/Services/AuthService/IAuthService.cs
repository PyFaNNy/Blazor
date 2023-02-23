using BlazorAppDemo.Application.Models;
using BlazorAppDemo.Application.Models.User;

namespace BlazorAppDemo.Client.Services.AuthService;

public interface IAuthService
{
    Task<ServiceResponse<int>> Register(UserRegister request);
    Task<ServiceResponse<string>> Login(UserLogin request);
    Task<ServiceResponse<bool>> ChangePassword(UserChangePassword request);
}