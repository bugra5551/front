using CarRentalAPI.Application.DTOs.CreateUpdate;
using CarRentalAPI.Application.DTOs.Read;

namespace CarRentalAPI.Application.Services;

/// <summary>
/// Çalışan CRUD operasyonları için servis arayüzü.
/// </summary>
public interface IEmployeeService : IService<EmployeeCreateUpdateDTO, EmployeeDTO> { }
