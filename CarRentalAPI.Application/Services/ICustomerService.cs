using CarRentalAPI.Application.DTOs.CreateUpdate;
using CarRentalAPI.Application.DTOs.Read;

namespace CarRentalAPI.Application.Services;

/// <summary>
/// Müşteri CRUD operasyonları için servis arayüzü.
/// </summary>
public interface ICustomerService : IService<CustomerCreateUpdateDTO, CustomerDTO> { }
