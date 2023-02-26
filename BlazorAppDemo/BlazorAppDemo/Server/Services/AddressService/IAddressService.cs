using BlazorAppDemo.Application.Models;
using BlazorAppDemo.Domain.Entity;

namespace BlazorAppDemo.Server.Services.AddressService;

public interface IAddressService
{
    Task<ServiceResponse<Address>> GetAddress();
    Task<ServiceResponse<Address>> AddOrUpdateAddress(Address address);
}