using BlazorAppDemo.Application.Models;

namespace BlazorAppDemo.Server.Services.CartService;

public interface ICartService
{
    Task<ServiceResponse<List<CartProductResponse>>> GetCartProducts(List<CartItem> cartItems);
}