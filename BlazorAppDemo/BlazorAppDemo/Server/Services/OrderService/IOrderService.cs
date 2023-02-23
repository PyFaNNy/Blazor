using BlazorAppDemo.Application.Models;
using BlazorAppDemo.Application.Models.Order;

namespace BlazorAppDemo.Server.Services.OrderService;

public interface IOrderService
{
    Task<ServiceResponse<bool>> PlaceOrder();
    Task<ServiceResponse<List<OrderOverviewResponse>>> GetOrders();
    Task<ServiceResponse<OrderDetailsResponse>> GetOrderDetails(int orderId);
}