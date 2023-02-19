using BlazorAppDemo.Application.Models;
using BlazorAppDemo.Domain;

namespace BlazorAppDemo.Server.Services.AuthService;

public interface IAuthService
{
    Task<ServiceResponse<int>> Register(User user, string password);
    Task<bool> UserExists(string email);
    Task<ServiceResponse<string>> Login(string email, string password);
}