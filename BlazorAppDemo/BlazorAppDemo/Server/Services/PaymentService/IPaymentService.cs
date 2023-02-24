using BlazorAppDemo.Application.Models;
using Stripe.Checkout;

namespace BlazorAppDemo.Server.Services.PaymentService;

public interface IPaymentService
{
    Task<Session> CreateCheckoutSession();
    Task<ServiceResponse<bool>> FulfillOrder(HttpRequest request);
}