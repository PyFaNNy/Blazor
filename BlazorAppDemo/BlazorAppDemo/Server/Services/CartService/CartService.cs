using BlazorAppDemo.Application.Interfaces;
using BlazorAppDemo.Application.Models;
using BlazorAppDemo.Domain.Entity;
using Microsoft.EntityFrameworkCore;

namespace BlazorAppDemo.Server.Services.CartService;

public class CartService : ICartService
{
    private readonly IBlazorDbContext _dbContext;
    private readonly IAuthService _authService;

    public CartService(IBlazorDbContext dbcontext, IAuthService authService)
    {
        _dbContext = dbcontext;
        _authService = authService;
    }
    
    public async Task<ServiceResponse<List<CartProductResponse>>> GetCartProducts(List<CartItem> cartItems)
    {
        var result = new ServiceResponse<List<CartProductResponse>>
        {
            Data = new List<CartProductResponse>()
        };

        foreach (var item in cartItems)
        {
            var product = await _dbContext.Products
                .Where(p => p.Id == item.ProductId)
                .FirstOrDefaultAsync();

            if (product == null)
            {
                continue;
            }

            var productVariant = await _dbContext.ProductVariants
                .Where(v => v.ProductId == item.ProductId
                            && v.ProductTypeId == item.ProductTypeId)
                .Include(v => v.ProductType)
                .FirstOrDefaultAsync();

            if (productVariant == null)
            {
                continue;
            }

            var cartProduct = new CartProductResponse
            {
                ProductId = product.Id,
                Title = product.Title,
                ImageUrl = product.ImageUrl,
                Price = productVariant.Price,
                ProductType = productVariant.ProductType.Name,
                ProductTypeId = productVariant.ProductTypeId,
                Quantity = item.Quantity
            };

            result.Data.Add(cartProduct);
        }

        return result;
    }

    public async Task<ServiceResponse<List<CartProductResponse>>> StoreCartItems(List<CartItem> cartItems)
    {
        cartItems.ForEach(cartItem => cartItem.UserId = _authService.GetUserId());
        _dbContext.CartItems.AddRange(cartItems);
        await _dbContext.SaveChangesAsync(new CancellationToken());

        return await GetDbCartProducts();
    }

    public async Task<ServiceResponse<int>> GetCartItemsCount()
    {
        var count = await _dbContext.CartItems.Where(x => x.UserId == _authService.GetUserId()).CountAsync();

        return new ServiceResponse<int>
        {
            Data = count
        };
    }

    public async Task<ServiceResponse<List<CartProductResponse>>> GetDbCartProducts(int? userId = null)
    {
        if(userId == null)
            userId = _authService.GetUserId();

        return await GetCartProducts(await _dbContext.CartItems
            .Where(ci => ci.UserId == userId).ToListAsync());
    }

    public async Task<ServiceResponse<bool>> AddToCart(CartItem cartItem)
    {
        cartItem.UserId = _authService.GetUserId();

        var sameItem = await _dbContext.CartItems
            .FirstOrDefaultAsync(x => x.ProductId == cartItem.ProductId &&
                                      x.ProductTypeId == cartItem.ProductTypeId &&
                                      x.UserId == cartItem.UserId);
        if (sameItem == null)
        {
            _dbContext.CartItems.Add(cartItem);
        }
        else
        {
            sameItem.Quantity += cartItem.Quantity;
        }

        await _dbContext.SaveChangesAsync(new CancellationToken());
        return new ServiceResponse<bool> {Data = true};
    }

    public async Task<ServiceResponse<bool>> UpdateQuantity(CartItem cartItem)
    {
        var dbCartItem = await _dbContext.CartItems
            .FirstOrDefaultAsync(x => x.ProductId == cartItem.ProductId &&
                                      x.ProductTypeId == cartItem.ProductTypeId &&
                                      x.UserId == _authService.GetUserId());

        if (dbCartItem is null)
        {
            return new ServiceResponse<bool>
            {
                Data = false,
                Message = "Cart item does not exist"
            };
        }

        dbCartItem.Quantity = cartItem.Quantity;
        await _dbContext.SaveChangesAsync(new CancellationToken());
        return new ServiceResponse<bool> {Data = true};
    }

    public async Task<ServiceResponse<bool>> RemoveItemFromCart(int productId, int productTypeId)
    {
        var dbCartItem = await _dbContext.CartItems
            .FirstOrDefaultAsync(x => x.ProductId == productId &&
                                      x.ProductTypeId == productTypeId &&
                                      x.UserId == _authService.GetUserId());

        if (dbCartItem is null)
        {
            return new ServiceResponse<bool>
            {
                Data = false,
                Message = "Cart item does not exist"
            };
        }

        _dbContext.CartItems.Remove(dbCartItem);
        await _dbContext.SaveChangesAsync(new CancellationToken());
        return new ServiceResponse<bool> {Data = true};
    }
}