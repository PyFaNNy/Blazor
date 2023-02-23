namespace BlazorAppDemo.Application.Models.Order;

public class OrderDetailsResponse
{
    public DateTime OrderDate { get; set; }
    public decimal TotalPrice { get; set; }
    public List<OrderDetailsProductResponse> Products { get; set; }
}