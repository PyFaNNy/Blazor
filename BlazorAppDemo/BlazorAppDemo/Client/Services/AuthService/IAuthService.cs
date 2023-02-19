using BlazorAppDemo.Application.Models;

namespace BlazorAppDemo.Client.Services.AuthService;

public interface IAuthService
{
    Task<ServiceResponse<int>> Register(UserRegister request);
}